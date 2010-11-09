using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;
using NHibernate.Criterion;

namespace Raisins.Client.Web.Models
{
	public class BeneficiaryModel
	{

		public long ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public BeneficiaryDetailModel Detail { get; set; }

	}

	public class BeneficiaryDetailModel
	{

		public decimal TotalAmount { get; set; }
		public long Votes { get; set; }

	}

}