using Microsoft.AspNetCore.Mvc;

// Diretivas que Devem ser referenciadas
using Microsoft.EntityFrameworkCore;
using BackEndProjetao.Data;
using BackEndProjetao.Data.Entities;

namespace BackEndProjetao.Controllers
{
    // É necessário fazer uso/referência ao atribuito [ApiController] que irá identificar este controller como o controller de uma API
    [ApiController]
    // Definir a rota que este controller assume quando a API é chamada.
    [Route("api/[controller]")]
    // Agora é necessário praticar o mecanismo de Herança com a superclasse ControllerBase
    public class StudentController : ControllerBase
    {
        // é necessário - dentro do controller - praticar a referencia de instancia da classe que auxiliará na manipulação do fluxo de dados - MyDbContext
        public readonly MyDbContext _dbContex;

        // definir o construtor da classe/controller/API para estabelecer a injeção mde dependencia
        public StudentController (MyDbContext dbContex)
        {
            // acessar a prop private e atribuila com o valor do paramentro indicado no construtor
            _dbContex = dbContex;
        }

        // ACTION QUE RECUPERA TODOS OS REGISTROS DA BASE -> Será responsável por fazer o uso da entity Student - que é a representação da table Student DB - para que cada uma das propriedades tenha seus valores, aqui, devidamente atribuidas

        // 1º passo: Definir o atributo [HttpGet] para que a tarefa assincrona recupere os dados
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // 2º passo: Definir uma prop, com o objetivo de receber de maneira assincrona - todos os registros da base de dados , a partir do acesso a entity
            var studentsList = await _dbContex.Student.ToListAsync();

            // 3º passo: retornar a lista de registros atribuidas a váriavel
            return Ok(studentsList);
        }
        // ACTION QUE RESGATA UM UNICO REGISTRO DA BASE - será responsavel por recuperar um unico registro bae - desde que esteja devidamente identificado - e disponibiliza-lo para o front

        // definir a tarefa assincrona para recuperar um unico registro da base - para esta funcionalidade ser executada plenamente é necessário, ja no atributo [HttpGet] indicar o parametro do registro
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegister(int id)
        {
            // 2º passo: definir uma prop para receber como valor - de maneira assincrona - um registro da base, acessando a entity,devidamente identificado com o parametro id
            var studentUnique = await _dbContex.Student.FindAsync(id);

            // 3º passo: verificar se o valor dado ao parametro id existe
            if(studentUnique == null)
            {
                return NotFound();
            }

            // 4º passo: se o valor do parametro id existir, o registro sera retornado para o front
            return Ok(studentUnique);
        }

        // ACTION PARA INSERIR UM REGISTRO NA BASE - será responsavel de forma assincrona - por inserir um registro na base a partir do acesso a entity Student

        // 1º passo: será usado - para definir esta tarefa assincrona - o atributo [HttpPost]. Dessa forma, a entity será acessada e os dados devem ser enviados
        [HttpPost]
        // para inserção de dados SEMPRE-SEMPRE-SEMPRE é necessário definir um parametro que receba os valores referenciados para o dito armazenamento.
        public async Task<IActionResult> Post(Student register)
        {
            // 2º passo: consiste em fazer o acesso a entity Student para que os dados recebidos pelo parametro possam ser enviados para a base.
            _dbContex.Student.Add(register);

            // 3º passo: de forma assincrona é preciso indicar que alteração - inserção de conjunto de valores - precisa persistir/ser salva e, assim a base é modificada com um novo registro.
            await _dbContex.SaveChangesAsync();

            // 4º passo: retornar o conjuto de valores inseridos.
            return Ok(register);
        }

        // ACTION PARA EXECUTAR A ATUALIZAÇÃO/ALTERAÇÃO DE UM REGISTRO - será responsável por, através do acesso entity, possibilitar a alteração/atualização em um registro.

        // 1º passo: definir uma tarefa assincrona, fazendo uso do atributo [HttpPut], para que o registro possa ser alterado.
        [HttpPut]
        public async Task<IActionResult> PutRegister(Student newRegister)
        {
            // 2º passo: Acessando a Entity, definir a operação necessária para a atualização/alteração dos dados
            _dbContex.Student.Update(newRegister);

            // de forma assincrona, fazer uso da operação que "salvamento" da alteração realizada.
            await _dbContex.SaveChangesAsync();

            // 3º passo: retornar o registro atualizado/alterado
            return Ok(newRegister);
        }

        // ACTION DE EXCLUSÃO DE REGISTRO - será resposavel por excluir um registro devidamente identificado e armazenado na base
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteRegister(int id)
        {
            // definir uma prop para receber como valor da busca de um registro identificado
            var delRegister = await _dbContex.Student.FindAsync(id);
            // verificar se o registro existe
            if (delRegister == null)
            {
                return NotFound();
            }
            // Aqui estaremos removendo o registro
            _dbContex.Student.Remove(delRegister);

            // de forma assincrona, fazer uso da operação que "salvamento" da alteração realizada.
            await _dbContex.SaveChangesAsync();

            // Retorna a Alteração
            return Ok();
        }
    }
}
