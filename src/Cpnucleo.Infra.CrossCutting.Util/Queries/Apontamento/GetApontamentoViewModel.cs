﻿namespace Cpnucleo.Infra.CrossCutting.Util.Queries.Apontamento;

public class GetApontamentoViewModel : BaseQuery
{
    public ApontamentoDTO Apontamento { get; set; }
    public OperationResult OperationResult { get; set; }
}
