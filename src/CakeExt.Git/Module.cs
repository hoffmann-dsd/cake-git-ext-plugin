using Cake.Core;
using Cake.Core.Composition;

namespace CakeExt.Git;

public class Module : ICakeModule
{
    private readonly ICakeContext _context;

    public Module(ICakeContext context)
    {
        _context = context;
    }

    public void Register(ICakeContainerRegistrar registrar)
    {
    }
}
