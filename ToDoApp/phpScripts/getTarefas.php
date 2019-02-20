<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$userID = $_POST["userID"];

    $statement = mysqli_prepare($con, "SELECT * FROM tarefas WHERE dono = '$userID'");
    mysqli_stmt_execute($statement);
    mysqli_stmt_store_result($statement);
    mysqli_stmt_bind_result($statement, $id, $dono, $titulo, $data, $descricao);
    $response = array();
    $rows = array();
    while(mysqli_stmt_fetch($statement)){
        $response["id"] = $id;
        $response["dono"] = $dono;
        $response["titulo"] = $titulo;
        $response["data"] = $data;
        $response["descricao"] = $descricao;
        array_push($rows, $response);
      }
    echo json_encode($rows);
?>
