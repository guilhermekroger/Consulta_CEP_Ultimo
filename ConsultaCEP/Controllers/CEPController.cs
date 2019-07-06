using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConsultaCEP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ConsultaCEP.Controllers
{
    public class CEPController : Controller
    {
        private readonly Context _context;

        public CEPController(Context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.Cep.ToList());
        }

        public IActionResult ConsultarCep(CEP Ceps)
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://viacep.com.br/ws/" + Ceps.Cep + "/json/");
            CEP Cep = JsonConvert.DeserializeObject<CEP>(json);
            _context.Cep.Add(Cep);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}