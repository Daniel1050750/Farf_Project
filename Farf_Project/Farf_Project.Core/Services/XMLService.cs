using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//<? xml version="1.0" standalone="yes"?>
//<DocumentElement>
//  <ExecFile>
//    <Activo>Unchecked</Activo>
//    <Instrução>0</Instrução>
//    <Descrição>/////////////////////////////////////////////</Descrição>
//    <Equipamento>System</Equipamento>
//    <Função>Sleep</Função>
//    <Argumento1>10000</Argumento1>
//    <Argumento2 />
//    <Argumento3 />
//    <Acção>Test</Acção>
//    <Tipo>Número</Tipo>
//    <Limite_Inferior>0</Limite_Inferior>
//    <Limite_Superior>0</Limite_Superior>
//    <Display xml:space="preserve"> </Display>
//  </ExecFile>
//</DocumentElement>

namespace Neadvance.Core
{
    class XMLService
    {
        static void Main(string[] args)
        {
            var XMLinstructions = new List<XMLInstruction>();
            for (int i = 0; i < 10; i++)
            {
                //for (int j = 0; j < 10; j++)
                //{
                XMLinstructions.Add(new XMLInstruction()
                {
                    Autoinc = string.Format("{0}", i),
                    //Tool = string.Format("Tool {0}", i),
                    //Function = string.Format("Function {0}", i)
                });
                //}
            }

            // Header
            var xml = new StringBuilder();
            xml.AppendLine("<?xml version=\"1.0\" standalone=\"yes\"?>");
            xml.AppendLine("<DocumentElement>");


            // Repeated Elements
            foreach (var XMLinstruction in XMLinstructions)
            {
                var instructionData = BuildInstructionData(XMLinstruction);
                xml.AppendLine(string.Format("  {0}", instructionData));
            }

            // Footer
            xml.Append("</DocumentElement>");


            // write to XML file
            string path = @"C:\main.xml";
            var myFile = File.CreateText(path);
            myFile.Close();
            File.WriteAllText(path, xml.ToString());          

            //Write to Console
            Console.WriteLine(xml.ToString());
            Console.ReadKey();
        }


        private static string BuildInstructionData(XMLInstruction XMLinstruction)
        {
            var instructionSb = new StringBuilder();

            // Get Random index from array
            Random rnd = new Random();

            string[] accao = { "Test", "Wait" };
            string[] equipamento = { "System", "Rearview", "PLC" };
            string[] funcaoSystem = { "Verify", "OpenFile", "SetVar", "GetErrorCount", "VerifySub", "Sleep", "EndCycle", "WaitKey", "AnalyseErrors" };
            string[] funcaoRearview = { "RvwGetResult", "RvwSaveImage", "RvwIsDataReady", "RvwAcquire", "RvwProcessFile", "RvwConfigCam", "RvwLoadImage" };
            string[] funcaoPLC = { "Read", "Write" };

            // make this repeat itself for generating different values for each time
            //decimal NearestToFive = Math.Round(new decimal(rnd.Next(0, 1000) / 10)) * 5);


            // Get Random index from array  TODO: generic mofo
            var randomIndex = rnd.Next(0, accao.Length);
            var randomIndex1 = rnd.Next(0, equipamento.Length);
            var randomIndex2 = rnd.Next(0, funcaoSystem.Length);
            var randomIndex3 = rnd.Next(0, funcaoRearview.Length);
            var randomIndex4 = rnd.Next(0, funcaoPLC.Length);

            // generate a random number between 0-1000 that ends in 5 or 0
            decimal randomNumber = rnd.Next(0, 1000);
            decimal NearestToFive = Math.Round(randomNumber / 10) * 5;

            instructionSb.AppendLine("<ExecFile>");

            // Checkbox
            instructionSb.AppendLine("<Activo>Checked</Activo>");

            // Autoincrement num
            instructionSb.AppendLine(string.Format("<Instrução>{0}</Instrução>", XMLinstruction.Autoinc));

            //<Descrição></Descrição>   text origin?
            instructionSb.AppendLine("<Descrição></Descrição>");

            // For each Equipamento value there's an array of Funcao available
            var equip = equipamento[randomIndex1];
            instructionSb.AppendLine(string.Format("<Equipamento>{0}</Equipamento>", equip)); // scalability
            if (equip == "System")
            {
                instructionSb.AppendLine(string.Format("<Função>" + funcaoSystem[randomIndex2] + "</Função>"));
            }
            if (equip == "Rearview")
            {
                instructionSb.AppendLine(string.Format("<Função>" + funcaoRearview[randomIndex3] + "</Função>"));
            }
            if (equip == "PLC")
            {
                instructionSb.AppendLine(string.Format("<Função>" + funcaoPLC[randomIndex4] + "</Função>"));
            }

            //<Argumento1>10000</Argumento1>    check rules  
            //<Argumento2/>                     check rules
            //<Argumento3/>                     check rules
            instructionSb.AppendLine(string.Format("<Argumento1>" + Math.Round(new decimal(rnd.Next(0, 1000) / 10)) * 5 + "</Argumento1>"));
            instructionSb.AppendLine(string.Format("<Argumento2>" + Math.Round(new decimal(rnd.Next(0, 1000) / 10)) * 5 + "</Argumento2>"));
            instructionSb.AppendLine(string.Format("<Argumento3>" + Math.Round(new decimal(rnd.Next(0, 1000) / 10)) * 5 + "</Argumento3>"));

            // if Wait Action == true then Wait else Test
            var TestWait = accao[randomIndex];
            instructionSb.AppendLine(string.Format("<Acção>" + TestWait + "</Acção>"));

            //instructionSb.AppendLine(string.Format("<instruction tool=\"{0}\"" + "\nfunction=\"{1}\">\n</instruction>", 
            //                                        instruction.Tool, 
            //                                        instruction.Function));

            // Assume all results are Número?
            instructionSb.AppendLine("<Tipo>Número</Tipo>");

            if (TestWait == "Test")
            {
                instructionSb.AppendLine(string.Format("<Limite_Inferior>" + Math.Round(new decimal(rnd.Next(0, 1000) / 10)) * 5 + "</Limite_Inferior>"));
                instructionSb.AppendLine(string.Format("<Limite_Superior>" + Math.Round(new decimal(rnd.Next(0, 1000) / 10)) * 5 + "</Limite_Superior>"));
            }
            else
            {
                //<Limite_Inferior>-99999</Limite_Inferior>             if (TestAction == true) get value, else -99999
                //<Limite_Superior>99999</Limite_Superior>              if (TestAction == true) get value, else 99999
                instructionSb.AppendLine("<Limite_Inferior>-99999</Limite_Inferior>");
                instructionSb.AppendLine("<Limite_Superior>99999</Limite_Superior>");
            }

            // <Display>$$</Display>
            // if Test Action == null then $$ = null
            // if Test Action OK == true then $$ = StringBuilder.Insert "-"
            // if Test Action NOK == true then $$ = StringBuilder.Insert "~"
            // if more than 1 parameter true returns from Test Action print all with ; as a separator
            instructionSb.AppendLine("<Display xml:space=\"preserve\"> </Display>");

            instructionSb.Append("</ExecFile>");

            return instructionSb.ToString();
        }
    }

    class XMLInstruction
    {
        public string Autoinc { get; set; }
        //public string Tool { get; set; }
        //public string Function { get; set; }
    }
}
