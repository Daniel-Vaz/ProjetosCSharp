<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$username = $_POST["username"];

    $statement = mysqli_prepare($con, "SELECT * FROM utilizadores WHERE user = ?");
    mysqli_stmt_bind_param($statement, "s", $username);
    mysqli_stmt_execute($statement);
    mysqli_stmt_store_result($statement);
    mysqli_stmt_bind_result($statement, $user_id, $username, $password);
    $response = array();
    while(mysqli_stmt_fetch($statement)){
        $response["user_id"] = $user_id;
        $response["username"] = $username;
        $response["password"] = $password;
      }
    echo json_encode($response);
?>
