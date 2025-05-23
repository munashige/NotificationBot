﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Extensions;

namespace NotificationsBot.Utils
{
    public static class Utilites
    {
        public static async Task<T> ToObject<T>(this HttpResponseMessage response)
        {
            string responseAsString = await response.Content.ReadAsStringAsync();
            T? responseObject = JsonSerializer.Deserialize<T>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            if (responseObject != null)
            {
                return responseObject;
            }
            throw new ArgumentException("Данные не удалось преобразовать в объект");
        }

        public static string PullRequestLinkConfigure(string project, string repoName, int pullrequestId, string linkLabel)
        {
            string configLink = Markdown.Escape($"https://tfs.dev.vitacore.ru/tfs/{project}/_git/{repoName}/pullrequest/{pullrequestId}");
            string label = Markdown.Escape(linkLabel);

            string link = $"[{label}]({configLink})";

            return link;
        }

        public static string WorkItemLinkConfigure(string project, string itemId, string linkLabel)
        {
            string configLink = Markdown.Escape($"https://tfs.dev.vitacore.ru/tfs/{project}/_workitems/edit/{itemId}");
            string label = Markdown.Escape(linkLabel);

            string link = $"[{label}]({configLink})";

            return link;
        }

        public static string ProjectLinkConfigure(string project, string repoName)
        {
            string configLink = Markdown.Escape($"https://tfs.dev.vitacore.ru/tfs/{project}/_git/{repoName}");
            string repository = Markdown.Escape(repoName);

            string link = $"[{repository}]({configLink})";

            return link;
        }

        public static string DeploymentLinkConfigure(string project, int releaseId, int envId, string _stage)
        {
            string configLink = Markdown.Escape($"https://tfs.dev.vitacore.ru/tfs/{project}/_releaseProgress?_a=release-environment-logs&releaseId={releaseId}&environmentId={envId}");
            string stage = Markdown.Escape(_stage);

            string link = $"[{stage}]({configLink})";

            return link;
        }

        public static string BuildLinkConfigure(string buildDefinition, string project, int buildId)
        {
            string configLink = Markdown.Escape($"https://tfs.dev.vitacore.ru/tfs/{project}/_build/results?buildId={buildId}&view=results");

            string stage = Markdown.Escape(buildDefinition);

            string link = $"[{stage}]({configLink})";

            return link;
        }
    }
}
