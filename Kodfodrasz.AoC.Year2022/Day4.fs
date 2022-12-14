module Kodfodrasz.AoC.Year2022.Day4

open System
open Kodfodrasz.AoC

type Sector = {
  From : int
  To   : int
}

type SectorAssignments = {
  First: Sector
  Second:Sector
}


let parseLine (line: string) : SectorAssignments =
  line.Split(',', (StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries))
  |> Seq.map(fun p ->
    let bounds = p.Split('-')
    {
      From = Int32.Parse(bounds[0])
      To   = Int32.Parse(bounds[1])
    })
  |> Seq.toList
  |> function
      | [first; second] -> {First = first; Second = second}
      | _ -> failwith "unexpected input format"
  

let parseInput (input: string): Result<SectorAssignments list, string> =
    input.Split('\n')
    |> Seq.map String.trim
    |> Seq.where String.notNullOrWhiteSpace
    |> Seq.map parseLine
    |> Seq.toList
    |> Ok


let answer1 sectors  =
  sectors
  |> Seq.map (fun (r :SectorAssignments) -> 
      match r with
      | r when r.First.From <= r.Second.From && r.First.To >= r.Second.To -> 1
      | r when r.Second.From <= r.First.From && r.Second.To >= r.First.To -> 1
      | _ -> 0)
  |> Seq.sum
  |> Ok

let between (s: Sector) x =
  s.From <= x && x <= s.To

let answer2 sectors  =
  sectors
  |> Seq.map (fun (r :SectorAssignments) -> 
      match r with
      | r when between r.First r.Second.From || between r.First r.Second.To -> 1
      | r when between r.Second r.First.From || between r.Second r.First.To -> 1
      | _ -> 0)
  |> Seq.sum
  |> Ok
  
type Solver() =
  inherit SolverBase("Camp Cleanup")
  with
    override this.Solve input =
      this.DoSolve parseInput [ answer1;  answer2  ] input
