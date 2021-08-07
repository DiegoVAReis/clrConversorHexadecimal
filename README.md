# clrConversorHexadecimal

## Conversor de String para Hexadecimal e de Hexadecimal para String

Este projeto é uma CLR SQL Function, onde após a geração da DLL, ela pode ser importada no banco de dados SQL Server (2008 ou superior), sendo uma função na qual é possível converter strings para hexadecimal. Por exemplo pode ser usada para converter caracteres arábes em hexadecimal, e com isso salvar no banco de dados apenas o valor em hexadecimal, e na hora de exibir esses dados isso ser convertido e mostrado para o usuário normalmente. 

Com o intuito de não se prender a ser necessário ter o Visual Studio instalado, foi disponibilizado apenas o arquivo .cs principal, e através deles podemos gerar nossa DLL que será importada no banco de dados. Caso não queira realizar a compilação pode ser utilizada a DLL já compilada aqui disponível na área de Release. 

Após esse contexto geral, vamos lá, irei separar em dois temas, primeiro a compilação da DLL e depois como instalar ela no banco de dados e usar: 

### Pré-Requisitos para compilação da DLL

1. Ter o .Net Framework 3.5 instalado (Pode ser baixado atráves desse link oficial da Microsoft)

Passo 1. 

Com o .Net Framework 3.5 instalado na máquina vamos configurar um PATH indicando o caminho necessário para acessar o compilador. 

Passo 2. 

Abra o CMD e entre na pasta na qual você esta com o arquivo clrConversorHexadecimal.cs, como no exemplo abaixo:

Após estar na pasta basta digitar o seguinte comando: ``

Dessa forma já será compilado a DDL no qual podemos usar para carregar no banco de dados, e ser disponibilizada como uma função para ser utilizada. 

Fonte: Para essa compilação, caso tenha mais dúvidas pode seguir o tutorial direto da Microsoft com maiores detalhes: ""

##

### Como utilizar a DLL e importar o CLR no banco de dados?

Para conseguirmos utilizar a DLL temos que seguir os seguintes passos:

Passo 1

Primeiramente temos que habilitar o CLR em nossa base de dados, atráves do comando:

Passo 2

Com o CLR habilitado precisamos importar a DLL, e para isso a DLL deve estar em alguma passa que o Servidor tenha acesso. Com isso iremos digitar o seguinte comando:


Obs: Substitua o caminho de exemplo acima, para o caminho no qual o seu servidor precise acessar para ler o arquivo. 


Passo 3

Após os passos acima, já será possível utilizar a função, veja que ela fica como uma Assembly no Banco de dados. 


### Como utilizo a função? 

A função pode ser utilizada como uma função normal do banco de dados, a diferença é que você não possui acesso ao fonte dela, ou seja, se você abrir a função com o comando `sp_helptext` você verá que a função está "vazia". 

Veja como passar os dados para que a função processe, é necessário passar o texto a ser convertido, passar o tipo de conversão, onde temos duas opções "" e "", e por fim devemos passar qual o separador que aqueles hexadecimals terão, a função aceita os seguintes separadores `_`, `:`, `;`, `|` e `-`. 

OBs: é necessário obrigatoriamente ter um separador, caso seja necessário você poderá customizar a função da forma que preferir mudando o separador, ou retirando a sua necessidade, porém lembre-se de ajustar os locais no qual validam 3 casos decimais, do contrário a função não terá o resultado esperado.

Ah!! Outro ponto importante é que o mesmo separador usado na conversão, é necessário ser passado na "desconversão", caso for passado outro separador, a função não terá o retorno esperado.

Veja exemplos de uso:




Viu como é simples? Bora usar então, e aproveitar. 








