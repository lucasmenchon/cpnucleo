﻿namespace Cpnucleo.Infra.CrossCutting.Util.Queries.Tarefa;

public class ListTarefaViewModel : BaseQuery
{
    public IEnumerable<TarefaDTO> Tarefas { get; set; }
    public OperationResult OperationResult { get; set; }
}
