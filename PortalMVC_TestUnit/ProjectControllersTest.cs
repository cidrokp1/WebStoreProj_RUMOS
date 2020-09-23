using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PortalMVC_TestUnit
{
    [TestClass]
    public class ProjectControllersTest
    {
        private readonly PortalMVC.Controllers.EmpregadosController _EmpregadosController;
        private readonly PortalMVC.Controllers.FaturasController _FaturasController;
        private readonly PortalMVC.Controllers.ProdutosController _ProdutosController;

        public ProjectControllersTest()
        {
            _EmpregadosController = new PortalMVC.Controllers.EmpregadosController();
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env = null;
            _FaturasController = new PortalMVC.Controllers.FaturasController(env);
            _ProdutosController = new PortalMVC.Controllers.ProdutosController();
        }


        [TestMethod]
        public void ProdutosControllerIndex_ReturnNotNullView()
        {
            var result = false;
            if (_ProdutosController.Index().GetType().Equals(new ViewResult())) 
            {
                result = true;
            }
            Assert.IsFalse(result, "The controller method should return a view.");
        }


        [TestMethod]
        public void FaturasControllerIndex_ReturnNotNullView()
        {
            var result = false;
            if (_FaturasController.Index().GetType().Equals(new ViewResult()))
            {
                result = true;
            }
            Assert.IsFalse(result, "The controller method should return a view.");
        }


        [TestMethod]
        public void EmpregadosControllerIndex_ReturnNotNullView()
        {
            var result = false;
            if (_EmpregadosController.Index().GetType().Equals(new ViewResult()))
            {
                result = true;
            }
            Assert.IsFalse(result, "The controller method should return a view.");
        }


       
    }
}
