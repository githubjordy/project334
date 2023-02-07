using FormsTagHelper.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using WebApplication234234.Models;
using HtmlAgilityPack;
using IronOcr;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace WebApplication234234.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            YgoDataViewModel model = new YgoDataViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateDeck(string DeckUrl, string DeckName)
        {

            var UrlOfDeck = DeckUrl;
            var NameOfDeck = DeckName;


            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://ygoprodeck.com/deck/traptrix-deck-2022-255163");

            var maindecknode = document.GetElementbyId("main_deck");
            // var cardss=node.GetClasses().Where(x => x == "ygodeckcard");

            var node = maindecknode.ChildNodes;
            var cards = node[2].ChildNodes;
            
            // var s = node.XPath;
            // var cards = document.DocumentNode.SelectNodes("//div[@class='ygodeckcard']");
            //var cards = maindecknode.SelectNodes("//div[@class='ygodeckcard']");


            /////////////////////////////////////////////////////////////////////////////////////////
            ///
            var deckId = new List<string>();
            List<ygocardinfomodel> Decknames = new List<ygocardinfomodel>();
             foreach (var card in cards)
             {
                 
                 var image = card.ChildNodes[0];
                var attributes = image.Attributes;
                var cardidattribute = attributes.Where(c => c.Name == "data-card").First();
                var cardvalue = cardidattribute.Value;
               
                deckId.Add(cardvalue);
               
                 //var ocr = new IronTesseract();

                /* using (var input = new OcrInput(path)) {

                     var result = ocr.Read(input);
                     result.SaveAsTextFile("C:\\duelingnexusdeckcreatorygo\\WebApplication234234\\WebApplication234234\\wwwroot\\textfilesforcards\\ygocards.txt");
                 }*/

             }

            using (StreamReader r = new StreamReader("C:\\duelingnexusdeckcreatorygo\\WebApplication234234\\WebApplication234234\\wwwroot\\textfilesforcards\\ygocards.json"))
            {
                string json = r.ReadToEnd();
                 Decknames = JsonConvert.DeserializeObject<List<ygocardinfomodel>>(json)
                    .Where(x => deckId.Contains(x.id.ToString())).ToList(); 

                   
            }

            List<Yugiohcard> deck = new List<Yugiohcard>();
            int deckcount=0;
            foreach (var cardname in Decknames) {

                
                Yugiohcard card = new Yugiohcard();

                var count =deckId.Count(x => x == cardname.id.ToString());

                card.count= count;
                deckcount= deckcount + card.count;
                card.name = cardname.name;
                deck.Add(card);


               
               
            }

            //////////////////////////////////////////////////////////////////////////////////////add deck to duelingnexus
            int x = 5;
           // httpwebrequest


            //HtmlNode[] nodes = document.DocumentNode.SelectNodes("//a").ToArray();
          


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}