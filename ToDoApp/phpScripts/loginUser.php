<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$username = $_POST["username"];
$password = $_POST["password"];

    $statement = mysqli_prepare($con, "SELECT * FROM utilizadores WHERE user = ? AND pass = ?");
    mysqli_stmt_bind_param($statement, "ss", $username, $password);
    mysqli_stmt_execute($statement);
    mysqli_stmt_store_result($statement);
    mysqli_stmt_bind_result($statement, $user_id, $username, $password);
    $response = array();
    $response["success"] = false;
    while(mysqli_stmt_fetch($statement)){
        $response["success"] = true;
        $response["user_id"] = $user_id;
        $response["username"] = $username;
        $response["password"] = $password;
      }
    echo json_encode($response);
?>
