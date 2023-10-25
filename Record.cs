using OfficeOpenXml;
using OfficeOpenXml.Style;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.MovementRecords;
using System;

namespace SaleSavvy_API
{
    public class Record : IRecord
    {
        IConfiguration _configuration;

        public Record(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Guid> GenerateExcelFile(List<OutputRecordStock> data)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> GenerateMovementRecordFile(List<OutputRecordStock> data)
        {
            var fileId = Guid.NewGuid();
            var destinationPath = _configuration["ExcelSettings:DestinationPath"];
            var excelFileName = $"{fileId}.xlsx";
            var excelFilePath = Path.Combine(destinationPath, excelFileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Dados");

                // Estilo do título do documento (fundo azul)
                var documentTitleStyle = worksheet.Cells["A1:G1"];
                documentTitleStyle.Merge = true; // Mescla as células
                documentTitleStyle.Style.Font.Size = 11; // tamanho da fonte
                documentTitleStyle.Style.Font.Bold = true; 
                documentTitleStyle.Style.Font.Color.SetColor(System.Drawing.Color.White); // Cor do texto (branco)
                documentTitleStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                documentTitleStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Blue); // Cor do fundo (azul)
                documentTitleStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // centralizando texto
                documentTitleStyle.Value = "RELATÓRIO DE ESTOQUE"; // Título do documento

                // Estilo do título das colunas
                var titleStyle = worksheet.Cells["A2:G2"].Style;
                titleStyle.Font.Size = 11;
                titleStyle.Font.Bold = true;
                titleStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Títulos das colunas
                worksheet.Cells["A2"].Value = "Nome do Produto";
                worksheet.Cells["B2"].Value = "Status de Movimento";
                worksheet.Cells["C2"].Value = "Data do Movimento";
                worksheet.Cells["D2"].Value = "Quantidade Atual";
                worksheet.Cells["E2"].Value = "Quantidade Movida";
                worksheet.Cells["F2"].Value = "Novo Valor";
                worksheet.Cells["G2"].Value = "Antigo Valor";

                int row = 3;
                foreach (var item in data)
                {
                    worksheet.Cells["A" + row].Value = item.Name;
                    worksheet.Cells["B" + row].Value = item.Status;
                    worksheet.Cells["C" + row].Value = item.DateMovement.ToString("dd/MM/yyyy");
                    worksheet.Cells["D" + row].Value = item.CurrentQuantity;
                    worksheet.Cells["E" + row].Value = item.MovementQuantity;
                    worksheet.Cells["F" + row].Value = item.NewValue;
                    worksheet.Cells["G" + row].Value = item.OldValue;
                    row++;
                }
                worksheet.Cells.AutoFitColumns();

                var excelFile = new FileInfo(excelFilePath);

                package.SaveAs(excelFile);
            }
            return fileId;
        }
    }
}
