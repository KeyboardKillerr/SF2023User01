using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Repositories;

public interface IUserRep : IReposBase<User>, IInsertable<User>
{ }
