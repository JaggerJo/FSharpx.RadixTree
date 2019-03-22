namespace FSharpx.RadixTree

[<Measure>]
type lvl

[<Measure>]
type pos

[<Measure>]
type items

type Node<'T> =
    | None
    | Branch of Node<'T> array
    | Leaf of 'T array

type RadixBalancedTree<'T>(radixBits: int, depth: int<lvl>, endIndex: int<pos>, root: Node<'T>) =
    member this.RadixBits = radixBits

    member this.Root: Node<'T> = root
    member this.Depth = depth
    member this.StartIndex = 0<pos>
    member this.EndIndex = endIndex

module RadixBalancedTree =
    [<Literal>]
    let DefaultIndexBlockSize = 5

    let empty<'T> = RadixBalancedTree<'T>(DefaultIndexBlockSize, 0<lvl>, 0<pos>, None)
    let emptyWithRadixBits<'T> (radixBits: int) = RadixBalancedTree<'T>(radixBits, 0<lvl>, 0<pos>, None)

    let radixBits (t: RadixBalancedTree<_>) = t.RadixBits
    let radix (t: RadixBalancedTree<_>) = (1 <<< t.RadixBits) - 1
    let length (t: RadixBalancedTree<_>) =  (t.EndIndex - t.StartIndex) * 1<items/pos>
    let depth (t: RadixBalancedTree<_>) = t.Depth
    let root (t: RadixBalancedTree<_>) = t.Root
    let startIndex (t: RadixBalancedTree<_>) = t.StartIndex
    let endIndex (t: RadixBalancedTree<_>) = t.EndIndex

    let append (newValue: 'T) (t: RadixBalancedTree<'T>) =
        let values = Array.zeroCreate (radix t)

        values.[0] <- newValue
        RadixBalancedTree<'T>(t.RadixBits, 1<lvl>, 1<pos>, Leaf values)
