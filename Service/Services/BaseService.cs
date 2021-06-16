using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Text;
using Infra.Data.Repositoriy;
using Service.Validators;
using AutoMapper;
using System.Linq;
using FluentValidation;

namespace Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TOutputModel Add<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            TEntity inserted = _baseRepository.Inserir(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(inserted);

            return outputModel;
        }

        public void Delete(int id) => _baseRepository.Deletar(id);

        public IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class
        {
            var entities = _baseRepository.Listar();

            var outputModels = entities.ToList().Select(s => _mapper.Map<TOutputModel>(s));

            return outputModels;
        }

        public TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = _baseRepository.Buscar(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public TOutputModel Update<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            TEntity updated = _baseRepository.Atualizar(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(updated);

            return outputModel;
        }
    }
}
