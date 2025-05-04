using AITUC.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AITUC.Services
{
    public class ReportService
    {
        public void GenerateCsvReport(IEnumerable<Member> members, string filePath)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Mobile,JoinDate");

            foreach (var member in members)
            {
                csv.AppendLine($"{member.MemberID},{member.MemberName},{member.Mobile},{member.CreatedDate:yyyy-MM-dd}");
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
