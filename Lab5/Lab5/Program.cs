using System.Collections.Immutable;
using System.Threading.Channels;
using Lab5.BeeAlgorithmm;
using Lab5.CliqueProblem;

Graph graph = new Graph(300,30);

var algo = new BeeAlgorithm(100,45,graph);
algo.Solve();

