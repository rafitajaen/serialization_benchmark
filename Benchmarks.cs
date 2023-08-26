using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace serialization_benchmark
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private GitHubModel model = new GitHubModel("HttpContext-not-thread-safe/3", new CommitModel("f672026c116a99d25fab0eb0942235d5c4b85b5d", "https://api.github.com/repos/dotnet/AspNetCore.Docs/commits/f672026c116a99d25fab0eb0942235d5c4b85b5d"), false);
        private string text = Newtonsoft.Json.JsonConvert.SerializeObject(new GitHubModel("HttpContext-not-thread-safe/3", new CommitModel("f672026c116a99d25fab0eb0942235d5c4b85b5d", "https://api.github.com/repos/dotnet/AspNetCore.Docs/commits/f672026c116a99d25fab0eb0942235d5c4b85b5d"), false));

        [Benchmark]
        public string Serialization_Text_Newtonsoft()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }

        [Benchmark]
        public string Serialization_Text_SystemTextJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(model);
        }

        [Benchmark]
        public string Serialization_Text_Jil()
        {
            return Jil.JSON.Serialize(model);
        }

        [Benchmark]
        public string Serialization_Text_Utf8Json()
        {
            return Utf8Json.JsonSerializer.ToJsonString(model);
        }

        [Benchmark]
        public GitHubModel Deserialization_Text_Newtonsoft()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GitHubModel>(text);
        }

        [Benchmark]
        public GitHubModel Deserialization_Text_SystemTextJson()
        {
            return System.Text.Json.JsonSerializer.Deserialize<GitHubModel>(text);
        }

        [Benchmark]
        public GitHubModel Deserialization_Text_Jil()
        {
            return Jil.JSON.Deserialize<GitHubModel>(text);
        }

        [Benchmark]
        public GitHubModel Deserialization_Text_Utf8Json()
        {
            return Utf8Json.JsonSerializer.Deserialize<GitHubModel>(text);
        }
    }

    public class GitHubModel
    {
        public string Name { get; set; }
        public CommitModel Commit { get; set; }
        public bool Protected { get; set; }

        public GitHubModel() { }

        public GitHubModel(string name, CommitModel commit, bool p)
        {
            this.Name = name;
            this.Commit = commit;
            this.Protected = p;
        }
    }


    public class CommitModel
    {
        public string Sha { get; set; }
        public string Url { get; set; }

        public CommitModel() { }
        public CommitModel(string sha, string url)
        {
            this.Sha = sha;
            this.Url = url;
        }
    }
}
