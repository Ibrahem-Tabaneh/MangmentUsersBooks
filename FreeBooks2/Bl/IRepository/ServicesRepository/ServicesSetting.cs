using Bl.Data;
using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesSetting : IServicesRepositorySetting<Setting>
    {
        private readonly FreeBookDbContext _context;

        public ServicesSetting(FreeBookDbContext context)
        {
            _context = context;
        }

        public Setting GetAll()
        {
            try
            {
                return _context.Settings.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Setting();
            }
           
        }

        public bool Save(Setting setting)
        {
            try
            {
                var oldSetting= GetAll();

                oldSetting.numberContact = setting.numberContact;
                oldSetting.Location = setting.Location;
                oldSetting.twitterLink = setting.twitterLink;
                oldSetting.facebookLink = setting.facebookLink;
                oldSetting.instagramLink = setting.instagramLink;
                oldSetting.Description = setting.Description;
                oldSetting.websiteName = setting.websiteName;
                oldSetting.linkedinLink = setting.linkedinLink;
                oldSetting.Email = setting.Email;
                oldSetting.Address= setting.Address;    

                _context.Update(oldSetting);
                _context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
