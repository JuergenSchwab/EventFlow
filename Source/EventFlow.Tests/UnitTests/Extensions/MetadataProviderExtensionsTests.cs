﻿// The MIT License (MIT)
// 
// Copyright (c) 2015-2016 Rasmus Mikkelsen
// Copyright (c) 2015-2016 eBay Software Foundation
// https://github.com/rasmus/EventFlow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.Configuration.Registrations;
using EventFlow.Core;
using EventFlow.EventStores;
using EventFlow.Extensions;
using EventFlow.TestHelpers;
using FluentAssertions;
using NUnit.Framework;

namespace EventFlow.Tests.UnitTests.Extensions
{
    [Category(Categories.Unit)]
    public class MetadataProviderExtensionsTests
    {
        [Test]
        public void AbstractMetadataProviderIsNotRegistered()
        {
            // Arrange
            var registry = new AutofacServiceRegistration();
            var sut = EventFlowOptions.New.UseServiceRegistration(registry);

            // Act
            Action act = () => sut.AddMetadataProviders(new List<Type>
            {
                typeof (AbstractTestSubscriber)
            });

            // Assert
            act.ShouldNotThrow<ArgumentException>();
        }
    }

    public abstract class AbstractTestMetadataProvider : IMetadataProvider
    {
        public abstract IEnumerable<KeyValuePair<string, string>> ProvideMetadata
            <TAggregate, TIdentity>(TIdentity id, IAggregateEvent aggregateEvent, IMetadata metadata)
            where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity;
    }
}
