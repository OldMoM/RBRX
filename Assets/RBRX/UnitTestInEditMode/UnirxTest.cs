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
        ///   <para>�����ಥ�¼�ʱ�����˴��¼���Where�������ᱻ����ִ���жϣ����ҵ�����������Subscribe</para>
        ///   <para>��ˣ�����Ч�ȿ��ֻ���ǵ��¼�-����Ϊ�ṹ�����ǲ��õ��¼�-���˷�-����Ϊ�ṹ</para>
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
