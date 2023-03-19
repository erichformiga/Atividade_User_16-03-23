using System;
using system.Collections.Generic;
using System.Linq;    
    
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Digite o número da opção desejada:");
            Console.WriteLine("1 - Criar usuário");
            Console.WriteLine("2 - Alterar usuário");
            Console.WriteLine("3 - Excluir usuário");
            Console.WriteLine("4 - Listar usuários");
            Console.WriteLine("5 - Contabilizar usuários por perfil");
            Console.WriteLine("6 - Contabilizar sessões");
            Console.WriteLine("0 - Sair");

            int opcao = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    CriarUsuario();
                    break;
                case 2:
                    AlterarUsuario();
                    break;
                case 3:
                    ExcluirUsuario();
                    break;
                case 4:
                    ListarUsuarios();
                    break;
                case 5:
                    ContabilizarUsuariosPorPerfil();
                    break;
                case 6:
                    ContabilizarSessoes();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine();
        }
    }