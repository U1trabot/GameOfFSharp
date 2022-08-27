open System
open System.Threading
let mutable matrix  = array2D [|
    [|0;0;0;0;0;0;|]
    [|0;0;0;0;0;0;|]
    [|0;0;1;1;0;0;|]
    [|0;1;0;0;1;0;|]
    [|0;0;1;0;1;0;|]
    [|0;0;0;1;0;0;|]
|]                             
let rec rowPrint (row:int[]) (str:string) (index:int) = if (row.Length > index) then
                                                            match row[index] with
                                                                | 1 -> rowPrint row (str+"⬛") (index+1)
                                                                | _ -> rowPrint row (str+"⬜") (index+1)
                                                                
                                                        else
                                                            str                                                                     
let matrixPrint (matrix:int[,]) = for i in 0..((Array2D.length1 matrix)-1) do
                                      Console.WriteLine(rowPrint matrix[i,*] "" 0)                                            
let conway (matrix:byref<int[,]>) = let future = Array2D.copy matrix
                                    for i in 0..((Array2D.length1 matrix)-1) do
                                        for c in 0..(matrix[i,*].Length-1) do
                                            match matrix[i,c] with                                          
                                                | 1 -> match ((try matrix[i-1,c-1]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i-1,c]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i-1,c+1] with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i,c-1]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i,c+1]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i+1,c-1] with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i+1,c]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i+1,c+1] with | :? IndexOutOfRangeException -> 0)) with
                                                        | 2 | 3 -> ()
                                                        | _ -> future[i,c] <- 0
                                                | _ -> match ((try matrix[i-1,c-1]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i-1,c]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i-1,c+1] with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i,c-1]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i,c+1]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i+1,c-1] with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i+1,c]   with | :? IndexOutOfRangeException -> 0)
                                                          + (try matrix[i+1,c+1] with | :? IndexOutOfRangeException -> 0)) with
                                                        | 3 -> future[i,c] <- 1
                                                        | _ -> ()
                                    future
while true do
    matrixPrint (matrix)
    Thread.Sleep(500)
    Console.Clear()
    matrix <- conway &matrix