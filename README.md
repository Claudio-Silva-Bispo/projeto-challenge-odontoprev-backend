# Projeto Backend Challenge Odontoprev

## Visão Geral

Este projeto é uma API para gerenciamento de  clientes, clínicas, consultas, feedback e outros recursos relacionados a um sistema odontológico. A API é construída usando ASP.NET Core e MongoDB para armazenamento de dados. Na segunda fase, iremos usar Oracle, pois nosso acesso está bloqueado.

## Nosso objetivo

Desenvolver uma aplicação móvel, gerenciada em Java, e uma aplicação web, gerenciada em ASP.NET / C#, com o objetivo de sugerir consultas para novos e antigos clientes utilizando inteligência artificial (IA). As sugestões de consultas serão baseadas na localidade preferida do cliente, nas avaliações de feedback das clínicas/especialistas e nos custos mais baixos. Com essa combinação, os clientes poderão realizar suas consultas de rotina de forma contínua, promovendo um ciclo de alta qualidade. Ao mesmo tempo, as clínicas e especialistas manterão um fluxo constante de clientes em suas carteiras.

# Link com vídeo do prótotipo da nossa aplicação

1. Será interessante para você entender melhor nossa aplicação, iniciando pelo Mobile.
2. Logo iremos criar o prótotipo da nossa aplicação na web. Será enviada na segunda sprint.

## Tecnologias Utilizadas
- ASP.NET Core
- MongoDB, não vamos usar o Oracle ainda, pois não é necessário.
- C#
- React para aplicação front-end

## Estrutura do Diretório

Nosso projeto será gerenciada com base na Clean Architecture, contendo interfaces dos repósitorios, mantendo a regra do clean code.

src/
├── Domain/                     -> Lógica de negócio e entidades
│   └── Entities/               -> Classes de domínio (models atuais)
│   └── Repositories/           -> Interfaces de repositórios
├── Application/                -> Casos de uso e lógica de aplicação
│   └── Services/               -> Casos de uso (services atuais)
│   └── DTOs/                   -> Objetos de transferência de dados
├── Infrastructure/             -> Implementação de repositórios, frameworks 
├── Web/                        -> API e interface de usuário
│   └── Controllers/            -> Controladores da API (controllers atuais)
└── Tests/                      -> Testes unitários e de integração


## Configuração e Execução

### Pré-requisitos

- .NET SDK
- MongoDB

### Configuração

1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/project-backend-challenge-odontoprev.git
   cd project-backend-challenge-odontoprev

## Execução

1. Restaure as dependências:
    $   dotnet restore

2. Compile e execute a aplicação:
    $   dotnet run

3. Acesse o Swagger UI para testar a API:
    $   http://localhost:3001/swagger

## Descrição de cada classe no detalhe

# Classe Cadastro

***Cadastro:*** 

Representa o cadastro de um usuário, coletando informações básicas através de um formulário. A classe é responsável por gerenciar os dados de entrada do usuário, como nome, e-mail e telefone. Seu principal objetivo é facilitar o processo de registro de novos usuários no sistema, permitindo a criação de contas de forma eficiente e organizada.

***DTOs (Data Transfer Objects)***

CadastroDTO: Um objeto de transferência de dados utilizado para encapsular os dados de cadastro que serão enviados e recebidos pela API. Essa classe ajuda a separar a lógica de negócios da apresentação, garantindo que apenas os dados necessários sejam expostos e manipulados durante o cadastro.

***Repositórios***

CadastroRepository: Interface que define as operações de acesso a dados para a entidade Cadastro. Esta classe permite a interação com a base de dados, incluindo operações como criar, ler, atualizar e deletar cadastros de usuários.

***Domains***

CadastroDomain: Representa a lógica de negócios associada ao processo de cadastro. Esta classe lida com as regras de validação e os fluxos necessários para garantir que os dados fornecidos pelos usuários sejam válidos e consistentes antes de serem salvos no banco de dados.

***Services***

CadastroService: Gerencia as operações relacionadas ao Cadastro, incluindo a criação de novos usuários, validação de dados e interação com o CadastroRepository para persistência. Esta classe atua como um intermediário entre o controlador e a camada de acesso a dados, garantindo que a lógica de negócios seja aplicada corretamente.

***Controllers***
CadastroController: Gerencia as requisições HTTP relacionadas ao Cadastro. Essa classe expõe endpoints para permitir que usuários criem novas contas, validando os dados recebidos e utilizando o CadastroService para processar as informações.

# Cliente

Responsabilidades da Classe Cliente:

A classe Cliente atua apenas como uma representação dos dados do cliente, contendo propriedades para armazenar informações relevantes. Nela vamos fazer atualização, deletar, usar com base na autenticação. Não terá o criar pois virá de Cadastro.

Um cliente pode ser apagado mas cadastro nunca.

