﻿using NUnit.Framework;
using StockSimulator.Domain.Entities;
using StockSimulator.Service.QuoteSimulator;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Tests.Service.QuoteSimulator
{
    [TestFixture]
    public class ListenerTest
    {
        const string url = "ws://localhost:8080/quotes";
        const string quote_name = "CMIG4";
        private Listener listener;
        [SetUp]
        public void Setup()
        {
            listener = new Listener();
        }

        [Test]
        public void Should_have_a_value()
        {
            var tarefa = listener.Listen(url, quote_name);

             while (listener?.Item == null)
                System.Threading.Thread.Sleep(500);

            listener.StopListening();
            Assert.IsTrue(listener.Item != null);
            Assert.Contains(quote_name, new List<string>() { ((Quote)listener.Item).Name });
        }

        [Test]
        public void Should_have_an_error_if_a_filter_is_empty()
        {
            var tarefa = listener.Listen(url, String.Empty);
            Assert.AreEqual(System.Threading.Tasks.TaskStatus.Faulted, tarefa.Status);
            listener.StopListening();
        }

        [Test]
        public void Should_have_an_error_if_a_filter_is_null()
        {
            var tarefa = listener.Listen(url, null);
            Assert.AreEqual(System.Threading.Tasks.TaskStatus.Faulted, tarefa.Status);
            listener.StopListening();
        }


        [Test]
        public void Should_have_an_error_if_a_url_is_null()
        {
            var tarefa = listener.Listen(null, quote_name);
            Assert.AreEqual(System.Threading.Tasks.TaskStatus.Faulted, tarefa.Status);
            listener.StopListening();
        }

        [Test]
        public void Should_have_an_error_if_a_url_is_empty()
        {
            var tarefa = listener.Listen(String.Empty, quote_name);
            Assert.AreEqual(System.Threading.Tasks.TaskStatus.Faulted, tarefa.Status);
            listener.StopListening();
        }


    }
}
