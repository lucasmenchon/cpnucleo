﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cpnucleo.Application.Interfaces;
using Cpnucleo.Domain.Interfaces;
using Cpnucleo.Domain.Models;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cpnucleo.Application.Services
{
    public class ImpedimentoTarefaAppService : CrudAppService<ImpedimentoTarefa, ImpedimentoTarefaViewModel>, IImpedimentoTarefaAppService
    {
        protected readonly IImpedimentoTarefaRepository _impedimentoTarefaRepository;

        public ImpedimentoTarefaAppService(IMapper mapper, ICrudRepository<ImpedimentoTarefa> repository, IUnitOfWork unitOfWork, IImpedimentoTarefaRepository impedimentoTarefaRepository)
            : base(mapper, repository, unitOfWork)
        {
            _impedimentoTarefaRepository = impedimentoTarefaRepository;
        }

        public IEnumerable<ImpedimentoTarefaViewModel> ListarPorTarefa(Guid idTarefa)
        {
            return _impedimentoTarefaRepository.ListarPorTarefa(idTarefa).ProjectTo<ImpedimentoTarefaViewModel>(_mapper.ConfigurationProvider).ToList();
        }
    }
}
