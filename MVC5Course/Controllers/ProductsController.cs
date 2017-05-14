using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModel;
using System.Data.Entity.Infrastructure;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Products
        [OutputCache(Duration = 5, Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public ActionResult Index(bool Active = true)
        {
            var data = repo.GetProduct列表頁所有資料(Active, showAll: false);

            ViewData.Model = data;

            ViewData["ppp"] = data;
            ViewBag.qqq = data;

            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = repo.Get單筆資料ByProductId(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult Create([Bind(Include = 
            "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = repo.Get單筆資料ByProductId(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            //[Bind(Include = "ProductId,ProductName,Price,Active,Stock")]Product product

            var product = repo.Get單筆資料ByProductId(id);

            //if (ModelState.IsValid)
            if (TryUpdateModel(product, 
                new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                //repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.Get單筆資料ByProductId(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListProducts(ProductListSearchVM searchCondition)
        {
            var data = repo.GetProduct列表頁所有資料(true);

            if (ModelState.IsValid)
            { 
                if (!String.IsNullOrEmpty(searchCondition.q))
                {
                    data = data.Where(p => p.ProductName.Contains(searchCondition.q));
                }

                data = data.Where(p => p.Stock > searchCondition.Stock_S && p.Stock < searchCondition.Stock_E);
            }

            ViewData.Model = data
                .Select(p => new ProductLiteVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductLiteVM data)
        {
            if (ModelState.IsValid)
            {
                // TODO: 儲存資料進資料庫

                TempData["CreateProduct_Result"] = "商品新增成功";

                return RedirectToAction("ListProducts");
            }
            // 驗證失敗, 繼續顯示原本的表單
            return View();
        }
    }
}
