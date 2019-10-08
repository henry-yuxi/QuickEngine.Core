namespace QuickEngine.Unity
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class QRoutines : CustomYieldInstruction
    {
        private static MonoBehaviour s_monoBehaviour;
        private MonoBehaviour m_monoBehaviour;
        private bool useStaticMonoBehaviour = true;
        private Queue<IEnumerator> queue;
        private IEnumerator current;
        private bool isRunning = false;
        private Coroutine coroutine;

        public MonoBehaviour MonoBehaviour
        {
            get
            {
                if (useStaticMonoBehaviour)
                {
                    if (s_monoBehaviour == null)
                    {
                        GameObject go = new GameObject("{QRoutines}");
                        UnityEngine.Object.DontDestroyOnLoad(go);
                        s_monoBehaviour = go.AddComponent<DummyMonoBehaviour>();
                        //Debug.Log("{QRoutines} singleton created.");
                    }
                    return s_monoBehaviour;

                }
                return m_monoBehaviour;
            }
            set
            {
                m_monoBehaviour = value;
                useStaticMonoBehaviour = m_monoBehaviour == null;
            }
        }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }

        public override bool keepWaiting
        {
            get
            {
                return isRunning;
            }
        }

        #region WaitFor aliases

        public QRoutines WaitFor(IEnumerator routine)
        {
            return WaitForRoutine(routine);
        }

        public QRoutines WaitFor(params IEnumerator[] routines)
        {
            for (int i = 0; i < routines.Length; i++)
            {
                WaitFor(routines[i]);
            }
            return this;
        }

        public QRoutines WaitFor(Action action, bool threaded = false)
        {
            return WaitForTask(action, threaded);
        }

        public QRoutines WaitFor(bool threaded = false, params Action[] actions)
        {
            return WaitForTasks(threaded, actions);
        }

        public QRoutines WaitFor(Animation animation)
        {
            return WaitForAnimation(animation);
        }

        public QRoutines WaitFor(AudioSource audioSource)
        {
            return WaitForAudioSource(audioSource);
        }

        public QRoutines WaitFor(Button button)
        {
            return WaitForButtonClick(button);
        }

        public QRoutines WaitFor(Collider collider)
        {
            return WaitForCollision(collider);
        }

        public QRoutines WaitFor(Collider2D collider2d)
        {
            return WaitForCollision2D(collider2d);
        }

        public QRoutines WaitFor(Thread thread)
        {
            return WaitForThread(thread);
        }

        public QRoutines WaitFor(UnityEvent unityEvent)
        {
            return WaitForEvent(unityEvent);
        }

        public QRoutines WaitFor(YieldInstruction instruction)
        {
            return WaitForYieldInstruction(instruction);
        }

        public QRoutines WaitFor(params YieldInstruction[] instructions)
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                WaitFor(instructions[i]);
            }
            return this;
        }

        public QRoutines WaitFor(QRoutines routine)
        {
            return WaitForQRoutine(routine);
        }

        public QRoutines WaitFor(params QRoutines[] routines)
        {
            for (int i = 0; i < routines.Length; i++)
            {
                WaitFor(routines[i]);
            }
            return this;
        }

        #endregion WaitFor aliases

        public static QRoutines Create(MonoBehaviour monoBehaviour = null)
        {
            return new QRoutines(monoBehaviour);
        }

        public static QRoutines Create(IEnumerator routine, MonoBehaviour monoBehaviour = null)
        {
            return new QRoutines(monoBehaviour).WaitFor(routine);
        }

        [Obsolete("Method Stop() has been deprecated. Use Abort() instead.")]
        public void Stop()
        {
            Abort();
        }

        public void Abort()
        {
            MonoBehaviour.StopCoroutine(coroutine);
            queue.Clear();
            isRunning = false;
            if (current.GetType() == typeof(WaitForTask))
            {
                ((WaitForTask)current).Stop();
            }
        }

        public QRoutines WaitForAnimation(Animation animation)
        {
            return WaitForRoutine(new WaitForAnimation(animation));
        }

        public QRoutines WaitForAudioSource(AudioSource audioSource)
        {
            return WaitForRoutine(new WaitForAudioSource(audioSource));
        }

        public QRoutines WaitForButtonClick(Button button)
        {
            return WaitForRoutine(new WaitForEvent(button.onClick));
        }

        public QRoutines WaitForCollision(Collider collider)
        {
            return WaitForRoutine(new WaitForCollision(collider));
        }

        public QRoutines WaitForCollision2D(Collider2D collider2d)
        {
            return WaitForRoutine(new WaitForCollision(collider2d));
        }

        public QRoutines WaitForEndOfFrame()
        {
            return WaitForYieldInstruction(Yielders.EndOfFrame);
        }

        public QRoutines WaitForFrames(int numberOfFrames)
        {
            for (int i = 0; i < numberOfFrames; i++)
            {
                WaitForEndOfFrame();
            }
            return this;
        }

        public QRoutines WaitForEvent(UnityEvent unityEvent)
        {
            return WaitForRoutine(new WaitForEvent(unityEvent));
        }

        public QRoutines WaitForFixedUpdate()
        {
            return WaitForYieldInstruction(Yielders.FixedUpdate);
        }

        public QRoutines WaitForKeyDown(string name)
        {
            return WaitForRoutine(new WaitForKeyDown(name));
        }

        public QRoutines WaitForKeyDown(KeyCode key)
        {
            return WaitForRoutine(new WaitForKeyDown(key));
        }

        public QRoutines WaitForMouseDown(int button)
        {
            return WaitForRoutine(new WaitForMouseDown(button));
        }

        public QRoutines WaitForRoutine(IEnumerator routine)
        {
            queue.Enqueue(routine);
            if (!isRunning)
            {
                coroutine = MonoBehaviour.StartCoroutine(routineMain());
                isRunning = true;
            }
            return this;
        }

        public QRoutines WaitForSeconds(float seconds)
        {
            return WaitForYieldInstruction(Yielders.Seconds(seconds));
        }

        public QRoutines WaitForSecondsRealtime(float seconds)
        {
            return WaitForRoutine(new WaitForSecondsRealtime(seconds));
        }

        public QRoutines WaitForTask(Action action, bool threaded = false)
        {
            return WaitForRoutine(new WaitForTask(threaded, action));
        }

        public QRoutines WaitForTasks(bool threaded = false, params Action[] actions)
        {
            return WaitForRoutine(new WaitForTask(threaded, actions));
        }

        public QRoutines WaitForThread(Thread thread)
        {
            return WaitForRoutine(new WaitForThread(thread));
        }

        public QRoutines WaitForQRoutine(QRoutines routine)
        {
            if (routine == this)
                Debug.LogWarning("QRoutine added to its own queue: QRoutine will be waiting for itself and will never resolve.");
            return WaitForRoutine(routine);
        }

        public QRoutines WaitForYieldInstruction(YieldInstruction yieldInstruction)
        {
            return WaitForRoutine(routineYieldInstruction(yieldInstruction));
        }

        public QRoutines WaitUntil(Func<bool> predicate)
        {
            return WaitForRoutine(new WaitUntil(predicate));
        }

        public QRoutines WaitWhile(Func<bool> predicate)
        {
            return WaitForRoutine(new WaitWhile(predicate));
        }

        private QRoutines(MonoBehaviour monoBehaviour)
        {
            this.MonoBehaviour = monoBehaviour;
            queue = new Queue<IEnumerator>();
        }

        private IEnumerator routineYieldInstruction(YieldInstruction yieldInstruction)
        {
            yield return yieldInstruction;
        }

        private IEnumerator routineMain()
        {
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                yield return current;
            }
            isRunning = false;
        }

        private class DummyMonoBehaviour : MonoBehaviour
        {

        }
    }
}
