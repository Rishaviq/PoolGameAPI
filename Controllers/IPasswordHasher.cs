﻿namespace PoolGameAPI.Controllers
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password,string hashedPassword);
    }
}
