<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$userID = $_POST["userID"];
$titulo = $_POST["titulo"];
$data = $_POST["data"];
$descricao = $_POST["descricao"];

    $statement = "INSERT INTO tarefas (id, dono, titulo, data, descrição) VALUES (NULL, '$userID', '$titulo', '$data', '$descricao')";
    if (mysqli_query($con,$statement))
    {
        echo "0";
    }
    else
    {
        echo "1";
    }
?>
