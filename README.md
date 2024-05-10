# UMBIT.ToDo

Projeto Template de Lista de Tarefas.

## Pré-requisitos

Antes de iniciar, certifique-se de ter instalado o seguinte:
- Docker: [Instalação do Docker](https://docs.docker.com/get-docker/)

## Instalação e Uso

1. Clone o repositório:

    ```bash
    git clone git@github.com:MRmachad/UMBIT.ToDo.git
    ```

2. Na raiz do projeto, execute o seguinte comando para construir os serviços do Docker conforme definido no arquivo `docker-compose.json`:

    ```bash
    docker compose -f docker-compose.json build
    ```

3. Após a construção, inicie os contêineres Docker:

    ```bash
    docker compose -f docker-compose.json up
    ```

## Contribuição

1. Faça o fork do projeto (<https://github.com/seu_usuario/seu_projeto/fork>)
2. Crie uma branch para sua modificação (`git checkout -b feature/nova-feature`)
3. Faça o commit (`git commit -am 'Adicione nova feature'`)
4. Faça o push para a branch (`git push origin feature/nova-feature`)
5. Crie um novo Pull Request

