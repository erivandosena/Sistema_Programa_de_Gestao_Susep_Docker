﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Susep.SISRH.Application.Queries.Abstractions;
using Susep.SISRH.Domain.AggregatesModel.CatalogoAggregate;
using Susep.SISRH.Domain.AggregatesModel.PlanoTrabalhoAggregate;
using SUSEP.Framework.Data.Abstractions.UnitOfWorks;
using SUSEP.Framework.Result.Concrete;
using System.Threading;
using System.Threading.Tasks;

namespace Susep.SISRH.Application.Commands.PlanoTrabalho
{
    public class CadastrarPlanoTrabalhoCustoCommandHandler : IRequestHandler<CadastrarPlanoTrabalhoCustoCommand, IActionResult>
    {
        private IPlanoTrabalhoRepository PlanoTrabalhoRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public CadastrarPlanoTrabalhoCustoCommandHandler(            
            IPlanoTrabalhoRepository planoTrabalhoRepository, 
            IUnitOfWork unitOfWork)
        {
            PlanoTrabalhoRepository = planoTrabalhoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Handle(CadastrarPlanoTrabalhoCustoCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>(request);            

            //Monta o objeto com os dados do catalogo
            var item = await PlanoTrabalhoRepository.ObterAsync(request.PlanoTrabalhoId);

            //Adiciona o custo
            item.AdicionarCusto(request.Valor, request.Descricao);

            //Altera o item de catalogo no banco de dados
            PlanoTrabalhoRepository.Atualizar(item);
            UnitOfWork.Commit(false);

            result.Result = true;
            result.SetHttpStatusToOk("Plano de trabalho alterado com sucesso.");
            return result;
        }
    }
}
