﻿using Cpnucleo.Shared.Queries.ListRecursoTarefaByTarefa;

namespace Cpnucleo.Application.Queries.ListRecursoTarefaByTarefa;

public sealed class ListRecursoTarefaByTarefaQueryValidator : AbstractValidator<ListRecursoTarefaByTarefaQuery>
{
    public ListRecursoTarefaByTarefaQueryValidator()
    {
        RuleFor(x => x.IdTarefa).NotEmpty();
    }
}
