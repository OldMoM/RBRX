using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;

namespace UnitTest
{
    public class UnirxTest
    {
        /// <summary>
        ///   <para>发生多播事件时，过滤此事件的Where操作符会被遍历执行判断，以找到符合条件的Subscribe</para>
        ///   <para>因此，在音效等框架只能是单事件-单行为结构，而非采用单事件-过滤符-多行为结构</para>
        /// </summary>
        [Test]
        public void Where_multicast_theCoditionSatisfiedBranchRun()
        {
            var subject = new Subject<int>();

            bool operator1Run = false;
            bool operator2Run = false;
            bool operator3Run = false;

            subject
            .Where(x =>
            {
                operator1Run = true;
                Debug.Log("Judgement branch 1");
                return x is 1;
            })
            .Subscribe(x =>
            {
                Debug.Log("Execute action 1");
            });

            subject
            .Where(x =>
            {
                operator2Run = true;
                Debug.Log("Judgement branch 2");
                return x is 2;
            })
            .Subscribe(x =>
            {
                Debug.Log("Execute action 2");
            });

            subject
            .Where(x =>
            {
                operator3Run = true;
                Debug.Log("Judgement branch 3");
                return x is 3;
            })
            .Subscribe(x =>
            {
                Debug.Log("Execute action 3");
            });

            subject.OnNext(1);
            subject.OnNext(3);

            Assert.IsTrue(operator1Run);
            Assert.IsTrue(operator2Run);
            Assert.IsTrue(operator3Run);
        }
    }
}
