using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using gerenciador_de_tarefas.Context;
using MigrationTarefa = gerenciador_de_tarefas.Migrations.Tarefa;
using ModelTarefa = gerenciador_de_tarefas.Models.Tarefa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gerenciador_de_tarefas.Controllers
{
    public class TarefaController : Controller
    {
        private readonly DataContext _dataContext;

        public TarefaController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var tarefas = _dataContext.Tarefas.ToList();
            return View(tarefas);
        }

        public IActionResult Details(int id)
        {
            var tarefas = _dataContext.Tarefas.Find(id);
            if (tarefas == null)
            {

                return RedirectToAction(nameof(Index));
            }

            return View(tarefas);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ModelTarefa tarefa)
        {


            if (ModelState.IsValid)
            {
                _dataContext.Tarefas.Add(tarefa);
                _dataContext.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(tarefa);
        }

        public IActionResult Edit(int id)
        {
            var tarefa = _dataContext.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost
        ]
        public IActionResult Edit(ModelTarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                var tarefaByData = _dataContext.Tarefas.Find(tarefa.Id);

                if (tarefaByData == null)
                { return NotFound(); }

                tarefaByData.Titulo = tarefa.Titulo;
                tarefaByData.Status = tarefa.Status;
                tarefaByData.Descricao = tarefa.Descricao;
                tarefaByData.Data = tarefa.Data;
                _dataContext.Tarefas.Update(tarefaByData);
                _dataContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);

        }

        public IActionResult delete(int id){
            var tarefa = _dataContext.Tarefas.Find(id);
            if(tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(tarefa);
        }

        [HttpPost]
        public IActionResult delete(ModelTarefa tarefa)
        {
            var tarefaByData = _dataContext.Tarefas.Find(tarefa.Id);
            
            _dataContext.Tarefas.Remove(tarefaByData);
            _dataContext.SaveChanges(true);
            return RedirectToAction(nameof(Index));
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}