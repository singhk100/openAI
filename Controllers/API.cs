using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace SmartUnderwriting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
       
        string endpoint = "https://smartunderwriter-semicolons.openai.azure.com/";
        string key = "940291b07a744dfcbf8ee0cc0923ebd7";
        string model = "Smartunderwriter-gpt35-turbo-16k";
        string odisha = "On 2 June 2023, three trains collided in Balasore district in odisha state of eastern India. The Coromandel Express entered the passing loop instead of the main line near Bahanaga Bazar railway station at full speed and collided with a goods train. Due to the high speed of the Coromandel Express, its 21 coaches derailed and three of those collided with the oncoming SMVT Bengaluru–Howrah Superfast Express on the adjacent track.\r\n\r\nA total of 296 people were killed in the crash and more than 1,200 others were injured.[4] It was India's deadliest railway crash since the Firozabad rail collision in 1995, although the Gaisal train collision in 1999 may have killed more people. It was also the deadliest rail disaster worldwide since the 2004 Sri Lanka tsunami train wreck.";
        string bihar = "Four people died and around 70 people were injured after the express train en route to Kamakhya junction in Assam from Delhi derailed in bihar at 9.35 pm on Wednesday. The derailment took place near Raghunathpur station of the Danapur division of the East Central Railway. Five trains have been cancelled so far while several diverted as restoration of the tracks is currently underway. The Patna-Puri special, Sasaram-Ara passenger special, Ara-BBU special, Patna-DDU special and Patna-BXR trains were cancelled and 12 trains were diverted in the down direction on the mainline, including Kota-Patna express, Delhi-KYQ express, CSMT-Asansol express,Magadh express, LTT-DBRG express after the accident.";

        string victums = "[\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"A\",\r\n\t\t\"Seat\\nNo.\": \"1\",\r\n\t\t\"Name\": \"ANUBHAV DAS\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"28\",\r\n\t\t\"Journey\\nUpto\": \"CTC\",\r\n\t\t\"Mobile\\nNumber\": \"7735229272\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"A\",\r\n\t\t\"Seat\\nNo.\": \"2\",\r\n\t\t\"Name\": \"ANSHUMAN PUROHI\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"46\",\r\n\t\t\"Journey\\nUpto\": \"BBS\",\r\n\t\t\"Mobile\\nNumber\": \"9556253876\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"A\",\r\n\t\t\"Seat\\nNo.\": \"3\",\r\n\t\t\"Name\": \"DIPALI CHAKRABO\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"64\",\r\n\t\t\"Journey\\nUpto\": \"VSKP\",\r\n\t\t\"Mobile\\nNumber\": \"9433615715\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"A\",\r\n\t\t\"Seat\\nNo.\": \"4\",\r\n\t\t\"Name\": \"SACHIN GUPTA\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"46\",\r\n\t\t\"Journey\\nUpto\": \"BAM\",\r\n\t\t\"Mobile\\nNumber\": \"9536821254\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"B\",\r\n\t\t\"Seat\\nNo.\": \"5\",\r\n\t\t\"Name\": \"SUMITA PAL\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"39\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"9962120743\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"B\",\r\n\t\t\"Seat\\nNo.\": \"6\",\r\n\t\t\"Name\": \"ANUSHKA PAL\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"12\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"9962120743\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"C\",\r\n\t\t\"Seat\\nNo.\": \"7\",\r\n\t\t\"Name\": \"MILI CHAKRABORT\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"64\",\r\n\t\t\"Journey\\nUpto\": \"VSKP\",\r\n\t\t\"Mobile\\nNumber\": \"9433615715\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"C\",\r\n\t\t\"Seat\\nNo.\": \"8\",\r\n\t\t\"Name\": \"MD A RAHMAN\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"72\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"9123638148\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"C\",\r\n\t\t\"Seat\\nNo.\": \"9\",\r\n\t\t\"Name\": \"P K CHAKRABORTY\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"76\",\r\n\t\t\"Journey\\nUpto\": \"VSKP\",\r\n\t\t\"Mobile\\nNumber\": \"9433615715\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"C\",\r\n\t\t\"Seat\\nNo.\": \"10\",\r\n\t\t\"Name\": \"RIDDHI GHOSH\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"23\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"9674464482\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"D\",\r\n\t\t\"Seat\\nNo.\": \"11\",\r\n\t\t\"Name\": \"KISHORE NAG\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"67\",\r\n\t\t\"Journey\\nUpto\": \"BZA\",\r\n\t\t\"Mobile\\nNumber\": \"9547793646\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"D\",\r\n\t\t\"Seat\\nNo.\": \"12\",\r\n\t\t\"Name\": \"RISHAN NAG\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"7\",\r\n\t\t\"Journey\\nUpto\": \"BZA\",\r\n\t\t\"Mobile\\nNumber\": \"9547793646\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"E\",\r\n\t\t\"Seat\\nNo.\": \"13\",\r\n\t\t\"Name\": \"SANCHITA NAG\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"65\",\r\n\t\t\"Journey\\nUpto\": \"BZA\",\r\n\t\t\"Mobile\\nNumber\": \"9547793646\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"E\",\r\n\t\t\"Seat\\nNo.\": \"14\",\r\n\t\t\"Name\": \"TANAYA NAG\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"32\",\r\n\t\t\"Journey\\nUpto\": \"BZA\",\r\n\t\t\"Mobile\\nNumber\": \"9547793646\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"F\",\r\n\t\t\"Seat\\nNo.\": \"15\",\r\n\t\t\"Name\": \"ARINDAM CHAKRAB\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"52\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"7003055415\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"F\",\r\n\t\t\"Seat\\nNo.\": \"16\",\r\n\t\t\"Name\": \"DOLON CHAKRABOR\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"47\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"7003055415\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"F\",\r\n\t\t\"Seat\\nNo.\": \"17\",\r\n\t\t\"Name\": \"ARYAN CHAKRABOR\",\r\n\t\t\"Gender\": \"M\",\r\n\t\t\"Age\": \"17\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"7003055415\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"1A\",\r\n\t\t\"Coach\": \"H1\",\r\n\t\t\"Cabin/\\nCoupe\": \"F\",\r\n\t\t\"Seat\\nNo.\": \"18\",\r\n\t\t\"Name\": \"ATRAYEE CHAKRAB\",\r\n\t\t\"Gender\": \"F\",\r\n\t\t\"Age\": \"21\",\r\n\t\t\"Journey\\nUpto\": \"MAS\",\r\n\t\t\"Mobile\\nNumber\": \"7003055415\"\r\n\t},\r\n\t{\r\n\t\t\"Class\": \"\"\r\n\t}\r\n]";
        [HttpPost]
        public async Task<IActionResult> GetResponseFromPdf( [FromBody] Person person)
        {
             
            string x = "";
            string[] words = person.name.Split(' ');
            var botResponse = "";
            if (words.Contains("odisha"))
            {

                OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = model,
                    Messages ={

                    new ChatRequestSystemMessage( "You are a helpful AI assistant"),
                    new ChatRequestAssistantMessage( "The following information is from the string given in the code " + odisha),
                    new ChatRequestUserMessage(person.name)
                },
                    MaxTokens = 1000,
                    Temperature = 0
                };

                Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);
                botResponse = response.Value.Choices.First().Message.Content;

            }
            else if (words.Contains("bihar"))
            {

                OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = model,
                    Messages ={

                    new ChatRequestSystemMessage( "You are a helpful AI assistant"),
                    new ChatRequestAssistantMessage( "The following information is from the string given in the code " + bihar),
                    new ChatRequestUserMessage(person.name)
                },
                    MaxTokens = 1000,
                    Temperature = 0
                };

                Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);
                botResponse = response.Value.Choices.First().Message.Content;

            }
            else if (words.Contains("victums"))
            {

                OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = model,
                    Messages ={

                    new ChatRequestSystemMessage( "You are a helpful AI assistant"),
                    new ChatRequestAssistantMessage( "the following information is in json string format and you have to give infromation using above json string" + victums),
                    new ChatRequestUserMessage(person.name)
                },
                    MaxTokens = 1000,
                    Temperature = 0
                };

                Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);
                botResponse = response.Value.Choices.First().Message.Content;

            }
            else
            {

                OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = model,
                    Messages ={

                    new ChatRequestSystemMessage( "You are a helpful AI assistant"),
                    new ChatRequestAssistantMessage( "The following information is related the given prompt"),
                    new ChatRequestUserMessage(person.name)
                },
                    MaxTokens = 1000,
                    Temperature = 0
                };

                Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);
                botResponse = response.Value.Choices.First().Message.Content;

            }
            Dictionary<string, string> dictionary_name = new Dictionary<string, string> { 
                { "prompt" , botResponse }
        };
            //return new JsonResult(botResponse);
            return Ok(dictionary_name);
        }

    }
    public class Person
    {
        public string name { get; set; }
    }
}


