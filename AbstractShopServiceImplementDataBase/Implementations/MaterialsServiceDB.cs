using AbstractGiftShopModel;
using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using AbstractGiftShopServiceDAL.ViewModel;
using AbstractGiftShopServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractGiftShopServiceImplementDataBase.Implementations
{
    public class MaterialsServiceDB : IMaterialsService
    {
        private AbstractGiftShopDbContext context;
        public MaterialsServiceDB(AbstractGiftShopDbContext context)
        {
            this.context = context;
        }
        public List<MaterialsViewModel> GetList()
        {
            List<MaterialsViewModel> result = context.Materialss.Select(rec => new
            MaterialsViewModel
            {
                Id = rec.Id,
                MaterialsName = rec.MaterialsName
            })
            .ToList();
            return result;
        }
        public MaterialsViewModel GetElement(int id)
        {
            Materials materials = context.Materialss.FirstOrDefault(rec => rec.Id == id);
            if (materials != null)
            {
                return new MaterialsViewModel
                {
                    Id = materials.Id,
                    MaterialsName = materials.MaterialsName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(MaterialsBindingModel model)
        {
            Materials materials = context.Materialss.FirstOrDefault(rec => rec.MaterialsName ==
            model.MaterialsName);
            if (materials != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            context.Materialss.Add(new Materials
            {
                MaterialsName = model.MaterialsName
            });
            context.SaveChanges();
        }
        public void UpdElement(MaterialsBindingModel model)
        {
            Materials materials = context.Materialss.FirstOrDefault(rec => rec.MaterialsName ==
            model.MaterialsName && rec.Id != model.Id);
            if (materials != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            materials = context.Materialss.FirstOrDefault(rec => rec.Id == model.Id);
            if (materials == null)
            {
                throw new Exception("Элемент не найден");
            }
            materials.MaterialsName = model.MaterialsName;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Materials Materials = context.Materialss.FirstOrDefault(rec => rec.Id == id);
            if (Materials != null)
            {
                context.Materialss.Remove(Materials);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}