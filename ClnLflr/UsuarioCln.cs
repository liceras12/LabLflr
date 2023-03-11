using CadLflr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnLflr
{
    public class UsuarioCln
    {
        public static int insertar(Usuario usuario)
        {
            using (var contexto = new LabLflrEntities())
            {
                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();
                return usuario.id;
            }
        }

        public static int actualizar(Usuario usuario)
        {
            using (var contexto = new LabLflrEntities())
            {
                var existente = contexto.Usuario.Find(usuario.id);
                existente.usuario1 = usuario.usuario1.Trim();
                existente.clave = usuario.clave.Trim();
                existente.rol=usuario.rol.Trim();
                existente.registroActivo = usuario.registroActivo;
                return contexto.SaveChanges();
            }
        }

        public static int cambiarClave(int id, string clave)
        {
            using (var contexto = new LabLflrEntities())
            {
                var existente = contexto.Usuario.Find(id);
                existente.clave = clave;
                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var contexto = new LabLflrEntities())
            {
                var existente = contexto.Usuario.Find(id);
                existente.registroActivo = false;
                return contexto.SaveChanges();
            }
        }

        public static Usuario get(int id)
        {
            using (var contexto = new LabLflrEntities())
            {
                return contexto.Usuario.Find(id);
            }
        }

        public static Usuario validar(string usuariod, string clave)
        {
            using (var contexto = new LabLflrEntities())
            {
                return contexto.Usuario.Where(x => x.usuario1 == usuariod && x.clave == clave && x.registroActivo == true).FirstOrDefault();
            }
        }

        public static List<Usuario> listar()
        {
            using (var contexto = new LabLflrEntities())
            {
                return contexto.Usuario.Where(x => x.registroActivo.Value).ToList();
            }
        }
    }
}
