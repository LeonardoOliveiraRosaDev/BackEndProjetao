using Microsoft.AspNetCore.Mvc;

// Diretivas que Devem ser referenciadas
using Microsoft.EntityFrameworkCore;
using BackEndProjetao.Data;
using BackEndProjetao.Data.Entities;
using System.Runtime.Intrinsics.Arm;


namespace BackEndProjetao.Controllers
{
    // É necessário fazer uso/referência ao atribuito [ApiController] que irá identificar este controller como o controller de uma API
    [ApiController]
    // Definir a rota que este controller assume quando a API é chamada.
    [Route("api/[controller]")]
    // Agora é necessário praticar o mecanismo de Herança com a superclasse ControllerBase
    public class CourseController : ControllerBase
    {

        public readonly MyDbContext _dbContex;


        public CourseController(MyDbContext dbContex)
        {
            _dbContex = dbContex;
        }
        
        // Action para Recuperar os dados
        [HttpGet]
        [Route("GetAll")] // Definindo uma rota para localização na API

        public async Task<IActionResult> Get()
        {
            // 2º passo: Definir uma prop, com o objetivo de receber de maneira assincrona - todos os registros da base de dados , a partir do acesso a entity
            var coursesList = await _dbContex.Course.ToListAsync();
            //coursesList = (float)(double)d["key"];
           
            // 3º passo: retornar a lista de registros atribuidas a váriavel
            return Ok(coursesList);
        }
        // ACTION QUE RESGATA UM UNICO REGISTRO DA BASE
        [HttpGet]
        [Route("GetOne/{id}")]// Definindo uma rota para localização na API
        public async Task<IActionResult> GetRegister(int id)
        {
            // 2º passo: definir uma prop para receber como valor - de maneira assincrona - um registro da base, acessando a entity,devidamente identificado com o parametro id
            var courseUnique = await _dbContex.Course.FindAsync(id);

            // 3º passo: verificar se o valor dado ao parametro id existe
            if (courseUnique == null)
            {
                return NotFound();
            }

            // 4º passo: se o valor do parametro id existir, o registro sera retornado para o front
            return Ok(courseUnique);
        }
        // ACTION PARA INSERIR UM REGISTRO NA BASE
        [HttpPost]
        [Route("AddRegister")]
       
        public async Task<IActionResult> Post(Course register)
        {
            // consiste em fazer o acesso a entity Course para que os dados recebidos pelo parametro possam ser enviados para a base.
            _dbContex.Course.Add(register);

         
            await _dbContex.SaveChangesAsync();

           
            return Ok(register);
        }
        // ACTION PARA EXECUTAR A ATUALIZAÇÃO/ALTERAÇÃO DE UM REGISTRO
        [HttpPut]
        [Route("UpRegister/{id}")]
        public async Task<IActionResult> PutRegister(int id, Course newRegister)
        {
            //  Acessando a Entity, definir a operação necessária para a atualização/alteração dos dados

            var foundRegister = await _dbContex.Course.FindAsync(id);
            // Verificar se o valor id existe
            if (foundRegister == null)
            {
                return NotFound();
            }
            // praticar a atribuição de novos valores às props existentes a partir dos valores recebidos pelo parametro newRegister
            foundRegister.NomeCurso = newRegister.NomeCurso;
            foundRegister.Mensalidade = newRegister.Mensalidade;           
            
            await _dbContex.SaveChangesAsync();

            //retornar o registro atualizado/alterado
            return Ok(newRegister);
        }
        // ACTION DE EXCLUSÃO DE REGISTRO
        [HttpDelete]
        [Route("delRegister/{id}")]
        public async Task<IActionResult> deleteRegister(int id)
        {
            // definir uma prop para receber como valor da busca de um registro identificado
            var delRegister = await _dbContex.Course.FindAsync(id);
            // verificar se o registro existe
            if (delRegister == null)
            {
                return NotFound();
            }
            // Aqui Remove o registro
            _dbContex.Course.Remove(delRegister);
           
            await _dbContex.SaveChangesAsync();

            
            return Ok();
        }

    }
}
