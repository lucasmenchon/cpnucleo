﻿namespace Cpnucleo.MVC.Controllers;

[Authorize]
public class TarefaController : BaseController
{
    private readonly ITarefaGrpcService _tarefaGrpcService;
    private readonly ISistemaGrpcService _sistemaGrpcService;
    private readonly IProjetoGrpcService _projetoGrpcService;
    private readonly IWorkflowGrpcService _workflowGrpcService;
    private readonly ITipoTarefaGrpcService _tipoTarefaGrpcService;

    private TarefaViewModel _viewModel;

    public TarefaController(IConfiguration configuration)
        : base(configuration)
    {
        _tarefaGrpcService = MagicOnionClient.Create<ITarefaGrpcService>(CreateAuthenticatedChannel());
        _sistemaGrpcService = MagicOnionClient.Create<ISistemaGrpcService>(CreateAuthenticatedChannel());
        _projetoGrpcService = MagicOnionClient.Create<IProjetoGrpcService>(CreateAuthenticatedChannel());
        _workflowGrpcService = MagicOnionClient.Create<IWorkflowGrpcService>(CreateAuthenticatedChannel());
        _tipoTarefaGrpcService = MagicOnionClient.Create<ITipoTarefaGrpcService>(CreateAuthenticatedChannel());
    }

    public TarefaViewModel ViewModel
    {
        get
        {
            if (_viewModel == null)
            {
                _viewModel = new TarefaViewModel();
            }

            return _viewModel;
        }
        set => _viewModel = value;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var result = await _tarefaGrpcService.ListTarefa(new ListTarefaQuery { GetDependencies = true });

            if (result.OperationResult == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return View();
            }

            ViewModel.Lista = result.Tarefas;

            return View(ViewModel);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Incluir()
    {
        await CarregarDados();

        ViewModel.User = HttpContext.User;

        return View(ViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Incluir(TarefaViewModel obj)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await CarregarDados();

                ViewModel.User = HttpContext.User;

                return View(ViewModel);
            }

            var result = await _tarefaGrpcService.CreateTarefa(new CreateTarefaCommand { Nome = obj.Tarefa.Nome, DataInicio = obj.Tarefa.DataInicio, DataTermino = obj.Tarefa.DataTermino, QtdHoras = obj.Tarefa.QtdHoras, Detalhe = obj.Tarefa.Detalhe, IdProjeto = obj.Tarefa.IdProjeto, IdWorkflow = obj.Tarefa.IdWorkflow, IdRecurso = obj.Tarefa.IdRecurso, IdTipoTarefa = obj.Tarefa.IdTipoTarefa });

            if (result == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return View();
            }

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Alterar(Guid id)
    {
        try
        {
            await CarregarDados(id);

            return View(ViewModel);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Alterar(TarefaViewModel obj)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await CarregarDados(obj.Tarefa.Id);

                return View(ViewModel);
            }

            var result = await _tarefaGrpcService.UpdateTarefa(new UpdateTarefaCommand { Id = obj.Tarefa.Id, Nome = obj.Tarefa.Nome, DataInicio = obj.Tarefa.DataInicio, DataTermino = obj.Tarefa.DataTermino, QtdHoras = obj.Tarefa.QtdHoras, Detalhe = obj.Tarefa.Detalhe, IdProjeto = obj.Tarefa.IdProjeto, IdWorkflow = obj.Tarefa.IdWorkflow, IdRecurso = obj.Tarefa.IdRecurso, IdTipoTarefa = obj.Tarefa.IdTipoTarefa });

            if (result == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return View();
            }

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Remover(Guid id)
    {
        try
        {
            await CarregarDados(id);

            return View(ViewModel);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Remover(TarefaViewModel obj)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await CarregarDados(obj.Tarefa.Id);

                return View(ViewModel);
            }

            var result = await _tarefaGrpcService.RemoveTarefa(new RemoveTarefaCommand { Id = obj.Tarefa.Id });

            if (result == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return View();
            }

            return RedirectToAction("Listar");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    private async Task CarregarDados(Guid? id = default)
    {
        if (id is not null)
        {
            var result = await _tarefaGrpcService.GetTarefa(new GetTarefaQuery { Id = id.Value });

            if (result.OperationResult == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return;
            }

            ViewModel.Tarefa = result.Tarefa;
        }

        var result2 = await _sistemaGrpcService.ListSistema(new ListSistemaQuery { });

        if (result2.OperationResult == OperationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
            return;
        }

        ViewModel.SelectSistemas = new SelectList(result2.Sistemas, "Id", "Nome");

        var result3 = await _projetoGrpcService.ListProjeto(new ListProjetoQuery { });

        if (result3.OperationResult == OperationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
            return;
        }

        ViewModel.SelectProjetos = new SelectList(result3.Projetos, "Id", "Nome");

        var result4 = await _workflowGrpcService.ListWorkflow(new ListWorkflowQuery { });

        if (result4.OperationResult == OperationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
            return;
        }

        ViewModel.SelectWorkflows = new SelectList(result4.Workflows, "Id", "Nome");

        var result5 = await _tipoTarefaGrpcService.ListTipoTarefa(new ListTipoTarefaQuery { });

        if (result5.OperationResult == OperationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
            return;
        }

        ViewModel.SelectTipoTarefas = new SelectList(result5.TipoTarefas, "Id", "Nome");
    }
}
