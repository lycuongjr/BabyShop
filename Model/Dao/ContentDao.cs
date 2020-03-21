using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString)); //giam dan
            }
            return model.OrderByDescending(x => x.CrratedDate).ToPagedList(page, pageSize);
        }

        ///// <summary>
        ///// List all by client
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <param name="page"></param>
        ///// <param name="pageSize"></param>
        ///// <returns></returns>
        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CrratedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Content> ListAllTag(string tag, int page, int pageSize)
        {
           var model = (from a in db.Contents join
                                        b in db.ContentTags
                                        on a.ID equals b.ContentID
                                        where b.TagID == tag
                                        select new
                                        {
                                            Name = a.Name,
                                            MetaTitle = a.MetaTitle,
                                            Image = a.Image,
                                            Description = a.Description,
                                            CreatedDate = a.CrratedDate,
                                            CreatedBy   = a.CreatedBy,
                                            ID          = a.ID

                                        }).AsEnumerable().Select(x=>new Content()
                                        {
                                            Name = x.Name,
                                            MetaTitle = x.MetaTitle,
                                            Image = x.Image,
                                            Description = x.Description,
                                            CrratedDate = x.CreatedDate,
                                            CreatedBy = x.CreatedBy,
                                            ID = x.ID
                                        });
            return model.OrderByDescending(x => x.CrratedDate).ToPagedList(page, pageSize);
        }
        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public long Create(Content content)
        {

            // alias
            if (!string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CrratedDate = DateTime.Now;


            db.Contents.Add(content);
            db.SaveChanges();
            // xu ly tag

            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(content.ID, tagId);

                }
            }
            return content.ID;
        }

        public long Edit(Content content)
        {

            // alias
            if (!string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CrratedDate = DateTime.Now;
            db.SaveChanges();
            // xu ly tag

            if (!string.IsNullOrEmpty(content.Tags))
            {
                this.RemoveAllContentTag(content.ID);
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(content.ID, tagId);

                }
            }
            return content.ID;
        }
        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }
        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();

        }
        public void InsertContentTag(int contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public List<Tag> ListTag(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
      on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });

            return model.ToList();
        }
    }
}
