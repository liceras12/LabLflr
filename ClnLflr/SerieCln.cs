using CadLflr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnLflr
{
    public class SerieCln
    {
        public static int insertar(Serie serie)
        {
            using (var contexto = new LabLflrEntities())
            {
                contexto.Serie.Add(serie);
                contexto.SaveChanges();
                return serie.id;
            }
        }

        public static int actualizar(Serie serie)
        {
            using (var contexto = new LabLflrEntities())
            {
                var existente = contexto.Serie.Find(serie.id);
                existente.titulo = serie.titulo.Trim();
                existente.sinopsis = serie.sinopsis.Trim();
                existente.director = serie.director.Trim();
                existente.duracion = serie.duracion;
                existente.fechaEstreno = serie.fechaEstreno;
                existente.usuarioRegistro = serie.usuarioRegistro;
                existente.registroActivo = serie.registroActivo;
                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id)
        {
            using (var contexto = new LabLflrEntities())
            {
                var existente = contexto.Serie.Find(id);
                existente.registroActivo = false;
                return contexto.SaveChanges();
            }
        }

        public static Serie get(int id)
        {
            using (var contexto = new LabLflrEntities())
            {
                return contexto.Serie.Find(id);
            }
        }

        public static List<Serie> listar()
        {
            using (var contexto = new LabLflrEntities())
            {
                return contexto.Serie.Where(x => x.registroActivo.Value).ToList();
            }
        }
    }
}
