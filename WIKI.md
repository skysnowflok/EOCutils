# Intro

Abaixo é a documentação para o uso do API, ela demostra as funções, seus campos, o que elas fazem, etc.

Na versão mais recente (0.1.0), Todas as funções estão presentes dentro da classe Interface, e todas retornam void. 

# Como instalar
No seu projeto, escreva no terminal:

```sh
dotnet add package SQLiteAPI --version 0.1.0
```

Após o processo terminar, adicione

```csharp 
using SQLiteAPI;
```

Em qualquer parte do seu codigo que seja necessario.

# Funções
## DatabaseCommand
args[0] é colocado em um switch para diferentes comandos, eles são:

### Create
- args[0] = "create"
- args[1] = string DatabaseName

Cria um arquivo .db com o nome especificado.

### Destroy
- args[0] = "destroy"
- args[1] = string DatabaseName

Deleta o arquivo .db especificado
### Selected
- args[0] = "select"
- args[1] = string DatabaseName

Escreve um arquivo .txt que contem o nome do banco de dados selecionado. Isso será usado para especificar qual banco de dados se referir quando utilizando a função TableCommand()

> [!IMPORTANT]
> É necessario selecionar um banco de dados antes de executar qualquer tipo de commando relacionado à tabelas


## TableCommand(params string[] args)

args[0] é colocado em um switch para diferentes programas, eles são:

### Create
- args[0] = "create"
- args[1] = string TableName
- args[2] = string TableSpecs

Cria uma tabela no banco de dados selecionado. TableSpecs necessita do uso de sintax de SQLite, e se refere à VALUES

Exemplo:

```csharp
SQLiteAPI.TableCommand(create, TabelaDeExemplo, "Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL");
```

Adiciona uma tabela com colunas Id e Username.
### Destroy
- args[0] = "destroy"
- args[1] = string TableName

Destroi a tabela especificada no banco dados selecionado.

### AddValue
- args[0] = "addvalue"
- args[1] = string TableName
- args[2] = string Columns
- args[3] = string Values

Adiciona valores à tabela TableName, nas colunas Columns com valores Values

### GetValue
- args[0] = "getvalue"
- args[1] = string Column
- args[2] = string TableName
- args[3] = string Id

Pega o valor de uma certa coluna com o Id especificado e os armazena em uma variavel statica da classe: GetValueResult.


### View
- args[0] = "view"
- args[1] = string TableName

Itera por cada valor e os escreve no terminal, da esquerda pra direita, de cima para baixo

# Solução de problemas
## File "selectedDatabase.txt" not found"

Certifique-se de que criou um banco de dados e o selecionou utilizando a função apropriada.