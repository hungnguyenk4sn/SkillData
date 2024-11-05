using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\Skill\SkillData.xml";
            string image = @"D:\Skill\Icon_1\";
            string image2 = @"D:\Skill\Icon_2\";
            string destinationFolder = @"D:\Skill\Icon3";

            XElement xElement = XElement.Load(filePath);

            // Duyệt qua các nút con
            foreach (XElement node in xElement.Elements("Skill"))
            {
                string soure = "";
                SkillData skillData = SkillData.Parse(node);
                if (skillData.Icon.Contains(".spr"))
                {
                   
                    soure += image2 + Path.GetFileNameWithoutExtension(skillData.Icon) + ".png";

                }
                else
                {
                   
                    soure = image + skillData.Icon + ".png";
                }
                skillData.Name = RemoveDiacritics(skillData.Name);
                // Tạo đường dẫn đầy đủ cho file đích
                string img = skillData.Name + ".png";
                string destinationFilePath = Path.Combine(destinationFolder, img);
                //Console.WriteLine(destinationFilePath+"    "+soure);
                node.Attribute("Icon").Value = skillData.Name;
                if (!File.Exists(soure))
                {
                    Console.WriteLine(soure);
                    continue;
                }

                File.Move(soure, destinationFilePath);
                

            }
            try
            {
                xElement.Save(filePath);
                Console.WriteLine("File đã được lưu thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi lưu file: {ex.Message}");
            }

            Console.ReadLine();
        }
       
        static string RemoveDiacritics(string text)
        {
            // Chuyển đổi sang dạng chuẩn NFC (Normalization Form C)
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                // Lấy các ký tự không phải là dấu
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Chuyển đổi lại về dạng chuẩn NFC
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(" ","_");
        }
    }
}
