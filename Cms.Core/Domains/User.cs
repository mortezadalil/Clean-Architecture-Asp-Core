using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Core.Domains
{
  public class User
  {
      public int Id { get; set; }
      public string Email { get; set; }
      public byte[] PasswordHash { get; set; }
      public byte[] PasswordSalt { get; set; }
      public string Phone { get; set; }


    }
}
