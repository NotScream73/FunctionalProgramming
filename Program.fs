// Задать интерпретацию команд и построить первые итерации
// в задаваемом количестве для следующей L-системы
// F++F++F++, F -> F-F++F-F

open System.Drawing

type Grammar = (char * char list) list

let FindSubst c (gr: Grammar) = 
   match List.tryFind (fun (x,S) -> x=c) gr with
     | Some(x,S) -> S
     | None -> [c]

let Apply (gr: Grammar) L =
   List.collect (fun c -> FindSubst c gr) L

let rec NApply n gr L =
   if n > 0 then
       let result = Apply gr L
       NApply (n - 1) gr result
   else
       L

let TurtleBitmapVisualizer n delta cmd =
    let W,H = 1600,1600
    let b = new Bitmap(W,H)
    let g = Graphics.FromImage(b)
    let pen = new Pen(Color.Black)
    g.Clear(Color.White)
    let NewCoord (x:float) (y:float) phi =
       let nx = x+n*cos(phi)
       let ny = y+n*sin(phi)
       (nx,ny,phi)
    let ProcessCommand x y phi = function
     | 'f' -> NewCoord x y phi
     | '+' -> (x,y,phi+delta)
     | '-' -> (x,y,phi-delta)
     | 'F' -> 
         let (nx,ny,phi) = NewCoord x y phi
         g.DrawLine(pen,(float32)x,(float32)y,(float32)nx,(float32)ny)
         (nx,ny,phi)
     | _ -> (x,y,phi)     
  
    let rec draw x y phi = function
     | [] -> ()
     | h::t ->
         let (nx,ny,nphi) = ProcessCommand x y phi h
         draw nx ny nphi t
    draw (float(W)/2.0) (float(H)/2.0) 0. cmd
    b

[<EntryPoint>]
let main argv =
    let lSystemInitial = "F++F++F++"
    let lSystemRules = [ ('F', ['F'; '-'; 'F'; '+'; '+'; 'F'; '-'; 'F']) ]
    let iterations = 3
    let lSystemResult = NApply iterations lSystemRules (lSystemInitial |> Seq.toList)

    let bitmap = TurtleBitmapVisualizer 40.0 (System.Math.PI / 3.0) lSystemResult

    bitmap.Save("D://system.jpeg")

    0