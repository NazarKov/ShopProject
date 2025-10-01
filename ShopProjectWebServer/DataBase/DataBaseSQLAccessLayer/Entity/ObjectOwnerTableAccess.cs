using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ObjectOwnerTableAccess : IObjectOwnerTableAccess 
    {

        private DbContextOptions<ContextDataBase> _option;
        public ObjectOwnerTableAccess(DbContextOptions<ContextDataBase> option)
        { 
            _option = option;
        } 

        public void Add(ObjectOwnerEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    context.ObjectOwners.Add(item);
                    
                    context.SaveChanges();
                }
            }
        }

        public void AddRange(IEnumerable<ObjectOwnerEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                    context.ObjectOwners.AddRange(items);
                    context.SaveChanges();
                }
            }
        }


        public void Delete(ObjectOwnerEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    if (context.ObjectOwners != null)
                    {
                        var operationsRecorders = context.ObjectOwners.Find(item.ID);
                        context.ObjectOwners.Remove(operationsRecorders);
                    }
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<ObjectOwnerEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                  
                    if (context.ObjectOwners.Count() != 0)
                    {
                        return context.ObjectOwners.ToList();
                    }
                    else
                    {
                        return new List<ObjectOwnerEntity>();
                    }
                }
                return null;
            }
        }

        public PaginatorData<ObjectOwnerEntity> GetAllPageColumn(double page, double countColumn, TypeStatusObjectOwner status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    if (context.ObjectOwners != null && context.ObjectOwners.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<ObjectOwnerEntity> result = new PaginatorData<ObjectOwnerEntity>();

                        if (status == TypeStatusObjectOwner.Unknown)
                        {
                            result.Page = (int)page;
                            result.Data = context.ObjectOwners.OrderBy(i => i.ID)
                                                          .Skip(countStart)
                                                          .Take((int)countColumn).ToList();

                            pages = context.ObjectOwners.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var objectOwners = context.ObjectOwners.Where(item => item.TypeStatus == status).ToList();
                            result.Data = objectOwners.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = objectOwners.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;
                    }
                    else
                    {
                        return new PaginatorData<ObjectOwnerEntity>();
                    }
                }
                return new PaginatorData<ObjectOwnerEntity>();
            }
        }

        public PaginatorData<ObjectOwnerEntity> GetObjectOwnerByNamePageColumn(string name, double page, double countColumn, TypeStatusObjectOwner status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    if (context.ObjectOwners != null && context.ObjectOwners.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<ObjectOwnerEntity> result = new PaginatorData<ObjectOwnerEntity>();

                        if (status == TypeStatusObjectOwner.Unknown)
                        {
                            result.Page = (int)page;
                            var objectOwnerEntity = context.ObjectOwners.Where(i => i.NameObject.Contains(name)).ToList();
                            result.Data = objectOwnerEntity.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = objectOwnerEntity.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var objectOwnerEntity = context.ObjectOwners.Where(t => t.TypeStatus == status)
                                                           .Where(i => i.NameObject.Contains(name)).ToList();
                            result.Data = objectOwnerEntity.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = objectOwnerEntity.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;

                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new PaginatorData<ObjectOwnerEntity>();
            }
        }

        public void Update(ObjectOwnerEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
