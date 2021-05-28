﻿using Cpnucleo.Infra.CrossCutting.Util.Commands.Responses.RecursoTarefa;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace Cpnucleo.Infra.CrossCutting.Util.Commands.Requests.RecursoTarefa
{
    [DataContract]
    public class CreateRecursoTarefaCommand : IRequest<CreateRecursoTarefaResponse>
    {
        [DataMember(Order = 1)]
        public RecursoTarefaViewModel RecursoTarefa { get; set; }
    }
}
