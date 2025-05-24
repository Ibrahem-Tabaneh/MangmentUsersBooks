using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entity
{
	public class Page2
	{
        public Guid Id { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("pageTittle"))]
		public string Tittle { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("pageDesc"))]
		public string Desc { get; set; }
        public string? ImageName { get; set; }
    }
}
