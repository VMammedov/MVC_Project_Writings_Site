using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TalentManager : ITalentService
    {
        ITalentDal _talentDal;

        public TalentManager(ITalentDal talentDal)
        {
            _talentDal = talentDal;
        }

        public Talent GetByID(int id)
        {
            return _talentDal.Get(x => x.TalentID == id);
        }

        public List<Talent> GetList()
        {
            return _talentDal.List(x=>x.TalentStatus==true);
        }

        public void TalentAdd(Talent talent)
        {
            _talentDal.Insert(talent);
        }

        public void TalentDelete(Talent talent)
        {
            talent.TalentStatus = false;
            _talentDal.Update(talent);
        }

        public void TalentUpdate(Talent talent)
        {
            _talentDal.Update(talent);
        }
    }
}
