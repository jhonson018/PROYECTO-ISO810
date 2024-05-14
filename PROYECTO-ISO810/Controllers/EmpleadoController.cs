using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using PROYECTO_ISO810.Models;

namespace PROYECTO_ISO810.Controllers
{
    public class EmpleadoController : Controller
    {
        private EmpleadoModel db = new EmpleadoModel();

        // GET: Empleado
        public ActionResult Index()
        {
            var empleados = db.Empleados.ToList();
            return View(empleados);
        }

        [HttpPost]
        public ActionResult ImportarArchivo(HttpPostedFileBase archivo)
        {
            if (archivo != null && archivo.ContentLength > 0)
            {
                using (var reader = new StreamReader(archivo.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var data = line.Split(','); // Suponiendo que los datos están separados por comas

                        var empleado = new Empleados
                        {
                            NombreCompleto = data[0],
                            Cedula = data[1],
                            Departamento = data[2],
                            Cargo = data[3],
                            Salario = decimal.Parse(data[4]),
                            FechaDeInicio = DateTime.Parse(data[5]),
                            FechaDeNacimiento = DateTime.Parse(data[6])
                        };

                        db.Empleados.Add(empleado);
                    }

                    db.SaveChanges();

                    TempData["Mensaje"] = "Importación exitosa.";
                }
            }
            else
            {
                TempData["Mensaje"] = "No se proporcionó un archivo válido.";
            }

            return RedirectToAction("Index");
        }


        public ActionResult GenerarArchivo()
        {
            // Recuperar los datos de los residentes desde la base de datos
            var empleados = db.Empleados.ToList();
            // Generar el contenido del archivo de texto
            var stringBuilder = new StringBuilder();
            foreach (var empleado in empleados)
            {
                stringBuilder.AppendLine($"{empleado.NombreCompleto}," + $"{empleado.Cedula}," +
                    $"{empleado.Departamento}," + $"{empleado.Cargo}," + $"{empleado.Salario}," + $"{empleado.FechaDeInicio}," + $"{empleado.FechaDeNacimiento}");
            }
            var contenidoArchivo = stringBuilder.ToString();
            // Escribir el contenido en un archivo
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\Jhonson\\Downloads\\Empleado.txt");
            System.IO.File.WriteAllText(filePath, contenidoArchivo);
            // Devolver el archivo para su descarga
            byte[] archivoBytes = System.IO.File.ReadAllBytes(filePath);
            return File(archivoBytes, "text/plain", "Empleado.txt");
        }
        public ActionResult GenerarArchivo2()
        {
            // Recuperar los datos de los residentes desde la base de datos
            var empleados = db.Empleados.ToList();
            // Generar el contenido del archivo de texto
            var stringBuilder = new StringBuilder();
            foreach (var empleado in empleados)
            {
                stringBuilder.AppendLine($"ID: {empleado.IDEmpleado}");
                stringBuilder.AppendLine($"Nombre Completo: {empleado.NombreCompleto}");
                stringBuilder.AppendLine($"Cedula: {empleado.Cedula}");
                stringBuilder.AppendLine($"Departamento: {empleado.Departamento}");
                stringBuilder.AppendLine($"Cargo: {empleado.Cargo}");
                stringBuilder.AppendLine($"Salario: {empleado.Salario}");
                stringBuilder.AppendLine($"Fecha De Inicio: {empleado.FechaDeInicio}");
                stringBuilder.AppendLine($"Fecha de Nacimiento: {empleado.FechaDeNacimiento}");
            }
            var contenidoArchivo = stringBuilder.ToString();
            // Escribir el contenido en un archivo
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\Jhonson\\Downloads\\Empleado.txt");
            System.IO.File.WriteAllText(filePath, contenidoArchivo);
            // Devolver el archivo para su descarga
            byte[] archivoBytes = System.IO.File.ReadAllBytes(filePath);
            return File(archivoBytes, "text/plain", "Empleado.txt");
        }
        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDEmpleado,NombreCompleto,Cedula,Departamento,Cargo,Salario,FechaDeInicio,FechaDeNacimiento")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleados);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDEmpleado,NombreCompleto,Cedula,Departamento,Cargo,Salario,FechaDeInicio,FechaDeNacimiento")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleados);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
