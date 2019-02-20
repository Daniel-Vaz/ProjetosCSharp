<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$id = $_POST["id"];
$titulo = $_POST["titulo"];
$data = $_POST["data"];
$descricao = $_POST["descricao"];

    $statement = "UPDATE tarefas SET titulo='$titulo', data='$data', descrição='$descricao' WHERE id = '$id'";
    if (mysqli_query($con,$statement))
    {
        echo "0";
    }
    else
    {
        echo "1";
    }
?>
