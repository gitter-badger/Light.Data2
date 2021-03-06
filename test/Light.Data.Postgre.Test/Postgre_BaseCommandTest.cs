﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading;
using System.Data;

namespace Light.Data.Postgre.Test
{
    public class Postgre_BaseCommandTest : BaseTest
    {
        public Postgre_BaseCommandTest(ITestOutputHelper output) : base(output)
        {
        }

        #region base test
        List<TeBaseField> CreateBaseFieldTableList(int count)
        {
            List<TeBaseField> list = new List<TeBaseField>();
            DateTime now = DateTime.Now;
            DateTime d = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            for (int i = 1; i <= count; i++) {
                int x = i % 5 == 0 ? -1 : 1;
                TeBaseField item = new TeBaseField();
                item.BoolField = i % 3 == 0;
                item.BoolFieldNull = i % 2 == 0 ? null : (bool?)(item.BoolField);
                item.ByteField = (byte)(i % 256);
                item.ByteFieldNull = i % 2 == 0 ? null : (byte?)(item.ByteField);
                item.SbyteField = (sbyte)((i % 128) * x);
                item.SbyteFieldNull = i % 2 == 0 ? null : (sbyte?)(item.SbyteField);
                item.Int16Field = (short)((i % 20) * x);
                item.Int16FieldNull = i % 2 == 0 ? null : (short?)(item.Int16Field);
                item.Int32Field = (int)((i % 23) * x);
                item.Int32FieldNull = i % 2 == 0 ? null : (int?)(item.Int32Field);
                item.Int64Field = (long)((i % 25) * x);
                item.Int64FieldNull = i % 2 == 0 ? null : (long?)(item.Int64Field);
                item.UInt16Field = (ushort)(i % 27);
                item.UInt16FieldNull = i % 2 == 0 ? null : (ushort?)(item.UInt16Field);
                item.UInt32Field = (uint)(i % 28);
                item.UInt32FieldNull = i % 2 == 0 ? null : (uint?)(item.UInt32Field);
                item.UInt64Field = (ulong)(i % 31);
                item.UInt64FieldNull = i % 2 == 0 ? null : (ulong?)(item.UInt64Field);
                item.FloatField = (float)((i % 19) * 0.1 * x);
                item.FloatFieldNull = i % 2 == 0 ? null : (float?)(item.FloatField);
                item.DoubleField = (double)((i % 22) * 0.1 * x);
                item.DoubleFieldNull = i % 2 == 0 ? null : (double?)(item.DoubleField);
                item.DecimalField = (decimal)((i % 26) * 0.1 * x);
                item.DecimalFieldNull = i % 2 == 0 ? null : (decimal?)(item.DecimalField);
                item.DateTimeField = d.AddMinutes(i * 2);
                item.DateTimeFieldNull = i % 2 == 0 ? null : (DateTime?)(item.DateTimeField);
                item.VarcharField = "testtest" + item.Int32Field;
                item.VarcharFieldNull = i % 2 == 0 ? null : item.VarcharField;
                item.TextField = "texttext" + item.Int32Field;
                item.TextFieldNull = i % 2 == 0 ? null : item.TextField;
                item.BigDataField = Encoding.UTF8.GetBytes(item.VarcharField);
                item.BigDataFieldNull = i % 2 == 0 ? null : item.BigDataField;
                item.EnumInt32Field = (EnumInt32Type)(i % 5 - 1);
                item.EnumInt32FieldNull = i % 2 == 0 ? null : (EnumInt32Type?)(item.EnumInt32Field);
                item.EnumInt64Field = (EnumInt64Type)(i % 5 - 1);
                item.EnumInt64FieldNull = i % 2 == 0 ? null : (EnumInt64Type?)(item.EnumInt64Field);
                list.Add(item);
            }
            return list;
        }

        List<TeBaseField> CreateAndInsertBaseFieldTableList(int count)
        {
            var list = CreateBaseFieldTableList(count);
            commandOutput.Enable = false;
            context.TruncateTable<TeBaseField>();
            context.BatchInsert(list);
            commandOutput.Enable = true;
            return list;
        }

        [Fact]
        public void TestCase_Specified_Context()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);

            List<TeBaseField> listEx = list;
            DataContext context1 = CreateBuilderContextByConnection();
            List<TeBaseField> listAc1 = context1.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc1);

            DataContext context2 = CreateBuilderContextByConfig();
            List<TeBaseField> listAc2 = context2.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc2);

            DataContext context3 = CreateBuilderContextByDi();
            List<TeBaseField> listAc3 = context3.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc3);

            DataContext context4 = CreateBuilderContextByDiConfigSpecified();
            List<TeBaseField> listAc4 = context4.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc4);

            DataContext context5 = CreateBuilderContextByDiConfigSpecifiedDefault();
            List<TeBaseField> listAc5 = context5.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc5);

            DataContext context6 = CreateBuilderContextByDiConfigGlobal();
            List<TeBaseField> listAc6 = context6.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc6);

            DataContext context7 = CreateBuilderContextByDiConfigGlobalDefault();
            List<TeBaseField> listAc7 = context7.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc7);

            DataContext context8 = CreateBuilderContextByConfigFile();
            List<TeBaseField> listAc8 = context8.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc8);
        }

        [Fact]
        public void TestCase_Specified_ContextFactory()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);

            List<TeBaseField> listEx = list;
            DataContext context1 = CreateBuilderContextFactoryByConnection();
            List<TeBaseField> listAc1 = context1.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc1);

            DataContext context2 = CreateBuilderContextFactoryByConfig();
            List<TeBaseField> listAc2 = context2.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc2);

            DataContext context3 = CreateBuilderContextFactoryByDi();
            List<TeBaseField> listAc3 = context3.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc3);

            DataContext context4 = CreateBuilderContextFactoryByDiConfigSpecified();
            List<TeBaseField> listAc4 = context4.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc4);

            DataContext context5 = CreateBuilderContextFactoryByDiConfigSpecifiedDefault();
            List<TeBaseField> listAc5 = context5.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc5);

            DataContext context6 = CreateBuilderContextFactoryByDiConfigGlobal();
            List<TeBaseField> listAc6 = context6.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc6);

            DataContext context7 = CreateBuilderContextFactoryByDiConfigGlobalDefault();
            List<TeBaseField> listAc7 = context7.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc7);

            DataContext context8 = CreateBuilderContextFactoryByConfigFile();
            List<TeBaseField> listAc8 = context8.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc8);
        }


        [Fact]
        public async Task TestCase_TruncateTable_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);
            await context.TruncateTableAsync<TeBaseField>(CancellationToken.None);
            List<TeBaseField> listAc;

            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(0, listAc.Count);
        }

        [Fact]
        public void TestCase_Query()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);

            List<TeBaseField> listEx = list;
            List<TeBaseField> listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            TeBaseField[] arrayEx = list.ToArray();
            TeBaseField[] arrayAc = context.Query<TeBaseField>().ToArray();
            AssertExtend.StrictEqual(arrayEx, arrayAc);
        }

        [Fact]
        public async Task TestCase_Query_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);

            List<TeBaseField> listEx = list;
            List<TeBaseField> listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.StrictEqual(listEx, listAc);

            TeBaseField[] arrayEx = list.ToArray();
            TeBaseField[] arrayAc = await context.Query<TeBaseField>().ToArrayAsync(CancellationToken.None);
            AssertExtend.StrictEqual(arrayEx, arrayAc);
        }

        [Fact]
        public void TestCase_Single()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);
            TeBaseField ex;
            TeBaseField ac;

            ex = list[0];
            ac = context.Query<TeBaseField>().First();
            AssertExtend.StrictEqual(ex, ac);

            ex = list[10];
            ac = context.Query<TeBaseField>().ElementAt(10);
            AssertExtend.StrictEqual(ex, ac);
        }

        [Fact]
        public async Task TestCase_Single_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);
            TeBaseField ex;
            TeBaseField ac;

            ex = list[0];
            ac = await context.Query<TeBaseField>().FirstAsync(CancellationToken.None);
            AssertExtend.StrictEqual(ex, ac);

            ex = list[10];
            ac = await context.Query<TeBaseField>().ElementAtAsync(10, CancellationToken.None);
            AssertExtend.StrictEqual(ex, ac);
        }

        [Fact]
        public void TestCase_Count_Exists()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);
            int ex;
            int ac;

            long exl;
            long acl;

            bool exe;
            bool ace;

            ex = list.Count;
            ac = context.Query<TeBaseField>().Count();
            AssertExtend.StrictEqual(ex, ac);

            exl = list.LongCount();
            acl = context.Query<TeBaseField>().LongCount();
            AssertExtend.StrictEqual(exl, acl);

            exe = list.Count > 0;
            ace = context.Query<TeBaseField>().Exists();
            AssertExtend.StrictEqual(exe, ace);
        }

        [Fact]
        public async Task TestCase_Count_Exists_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(45);
            int ex;
            int ac;

            long exl;
            long acl;

            bool exe;
            bool ace;

            ex = list.Count;
            ac = await context.Query<TeBaseField>().CountAsync(CancellationToken.None);
            AssertExtend.StrictEqual(ex, ac);

            exl = list.LongCount();
            acl = await context.Query<TeBaseField>().LongCountAsync(CancellationToken.None);
            AssertExtend.StrictEqual(exl, acl);

            exe = list.Count > 0;
            ace = await context.Query<TeBaseField>().ExistsAsync(CancellationToken.None);
            AssertExtend.StrictEqual(exe, ace);
        }

        [Fact]
        public void TestCase_CUD_Single()
        {
            context.TruncateTable<TeBaseField>();
            var item1 = CreateBaseFieldTableList(1)[0];
            var retInsert = context.Insert(item1);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = context.SelectById<TeBaseField>(item1.Id);
            AssertExtend.StrictEqual(item1, item2);
            item2.DateTimeField = GetNow();
            item2.DateTimeFieldNull = null;
            item2.Int32Field = 2;
            item2.Int32FieldNull = null;
            item2.DoubleField = 2.0d;
            item2.DoubleFieldNull = null;
            item2.VarcharField = "abc";
            item2.VarcharFieldNull = null;
            item2.EnumInt32Field = EnumInt32Type.Zero;
            item2.EnumInt32FieldNull = null;
            item2.EnumInt64Field = EnumInt64Type.Zero;
            item2.EnumInt64FieldNull = null;
            var retUpdate = context.Update(item2);
            Assert.Equal(1, item2.Id);
            Assert.Equal(1, retUpdate);
            var item3 = context.SelectById<TeBaseField>(item1.Id);
            AssertExtend.StrictEqual(item2, item3);
            var retDelete = context.Delete(item3);
            Assert.Equal(1, item3.Id);
            Assert.Equal(1, retDelete);
            var item4 = context.SelectById<TeBaseField>(item1.Id);
            Assert.Null(item4);
        }

        [Fact]
        public async Task TestCase_CUD_Single_Async()
        {
            context.TruncateTable<TeBaseField>();
            var item1 = CreateBaseFieldTableList(1)[0];
            var retInsert = await context.InsertAsync(item1, CancellationToken.None);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = await context.SelectByIdAsync<TeBaseField>(item1.Id, CancellationToken.None);
            AssertExtend.StrictEqual(item1, item2);
            item2.DateTimeField = GetNow();
            item2.DateTimeFieldNull = null;
            item2.Int32Field = 2;
            item2.Int32FieldNull = null;
            item2.DoubleField = 2.0d;
            item2.DoubleFieldNull = null;
            item2.VarcharField = "abc";
            item2.VarcharFieldNull = null;
            item2.EnumInt32Field = EnumInt32Type.Zero;
            item2.EnumInt32FieldNull = null;
            item2.EnumInt64Field = EnumInt64Type.Zero;
            item2.EnumInt64FieldNull = null;
            var retUpdate = await context.UpdateAsync(item2, CancellationToken.None);
            Assert.Equal(1, item2.Id);
            Assert.Equal(1, retUpdate);
            var item3 = await context.SelectByIdAsync<TeBaseField>(item1.Id, CancellationToken.None);
            AssertExtend.StrictEqual(item2, item3);
            var retDelete = await context.DeleteAsync(item3, CancellationToken.None);
            Assert.Equal(1, item3.Id);
            Assert.Equal(1, retDelete);
            var item4 = await context.SelectByIdAsync<TeBaseField>(item1.Id, CancellationToken.None);
            Assert.Null(item4);
        }

        [Fact]
        public void TestCase_CUD_Single_NoIdentity()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
            item1.Id = 0;
            item1.Int32Field = 1;
            item1.DoubleField = 0.1;
            item1.VarcharField = "level1";
            item1.DateTimeField = GetNow();
            item1.EnumInt32Field = EnumInt32Type.Positive1;
            var retInsert = context.Insert(item1);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = context.SelectByKey<TeBaseFieldNoIdentity>(item1.Id);
            AssertExtend.StrictEqual(item1, item2);
            item2.DateTimeField = GetNow();
            item2.Int32Field = 2;
            item2.VarcharField = "level2";
            item2.DoubleField = 0.2;
            item2.EnumInt32Field = EnumInt32Type.Negative1;
            var retUpdate = context.Update(item2);
            Assert.Equal(0, item2.Id);
            Assert.Equal(1, retUpdate);
            var item3 = context.SelectByKey<TeBaseFieldNoIdentity>(item1.Id);
            AssertExtend.StrictEqual(item2, item3);
            var retDelete = context.Delete(item3);
            Assert.Equal(0, item3.Id);
            Assert.Equal(1, retDelete);
            var item4 = context.SelectByKey<TeBaseFieldNoIdentity>(item1.Id);
            Assert.Null(item4);
        }

        [Fact]
        public async Task TestCase_CUD_Single_NoIdentity_Async()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
            item1.Id = 0;
            item1.Int32Field = 1;
            item1.DoubleField = 0.1;
            item1.VarcharField = "level1";
            item1.DateTimeField = GetNow();
            item1.EnumInt32Field = EnumInt32Type.Positive1;
            var retInsert = await context.InsertAsync(item1, CancellationToken.None);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = await context.SelectByKeyAsync<TeBaseFieldNoIdentity>(item1.Id, CancellationToken.None);
            AssertExtend.StrictEqual(item1, item2);
            item2.DateTimeField = GetNow();
            item2.Int32Field = 2;
            item2.VarcharField = "level2";
            item2.DoubleField = 0.2;
            item2.EnumInt32Field = EnumInt32Type.Negative1;
            var retUpdate = await context.UpdateAsync(item2, CancellationToken.None);
            Assert.Equal(0, item2.Id);
            Assert.Equal(1, retUpdate);
            var item3 = await context.SelectByKeyAsync<TeBaseFieldNoIdentity>(item1.Id, CancellationToken.None);
            AssertExtend.StrictEqual(item2, item3);
            var retDelete = await context.DeleteAsync(item3, CancellationToken.None);
            Assert.Equal(0, item3.Id);
            Assert.Equal(1, retDelete);
            var item4 = await context.SelectByKeyAsync<TeBaseFieldNoIdentity>(item1.Id, CancellationToken.None);
            Assert.Null(item4);
        }

        [Fact]
        public void TestCase_CUD_Bulk_NoIdentity()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            int count = 50;
            List<TeBaseFieldNoIdentity> list = new List<TeBaseFieldNoIdentity>();
            for (int i = 0; i < count; i++) {
                var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
                item1.Id = i + 1;
                item1.Int32Field = 1;
                item1.DoubleField = 0.1;
                item1.VarcharField = "level1";
                item1.DateTimeField = GetNow();
                item1.EnumInt32Field = EnumInt32Type.Positive1;
                list.Add(item1);
            }
            var retInsert = context.BatchInsert(list, 2, 38);
            Assert.Equal(38, retInsert);
            var ac = context.Query<TeBaseFieldNoIdentity>().ToList();
            var ex = list.Skip(2).Take(38).ToList();
            AssertExtend.StrictEqual(ex, ac);
        }

        [Fact]
        public void TestCase_CUD_Bulk_NoIdentityNull()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            int count = 50;
            List<TeBaseFieldNoIdentity> list = new List<TeBaseFieldNoIdentity>();
            for (int i = 0; i < count; i++) {
                var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
                item1.Id = i + 1;
                item1.Int32Field = 1;
                item1.DoubleField = 0.1;
                item1.VarcharField = "level1";
                item1.DateTimeField = GetNow();
                item1.EnumInt32Field = EnumInt32Type.Positive1;
                list.Add(item1);
            }
            list[0] = null;
            list[2] = null;
            list[28] = null;
            var retInsert = context.BatchInsert(list, 2, 38);
            Assert.Equal(36, retInsert);
            var ac = context.Query<TeBaseFieldNoIdentity>().ToList();
            var ex = list.Skip(2).Take(38).Where(x => x != null).ToList();
            AssertExtend.StrictEqual(ex, ac);

            list.Clear();
            Assert.Equal(0, context.BatchInsert(list));
            for (int i = 0; i < count; i++) {
                list.Add(null);
            }
            Assert.Equal(0, context.BatchInsert(list));
            Assert.Equal(0, context.BatchInsert(list, 6, 10));
        }

        [Fact]
        public async Task TestCase_CUD_Bulk_NoIdentity_Async()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            int count = 50;
            List<TeBaseFieldNoIdentity> list = new List<TeBaseFieldNoIdentity>();
            for (int i = 0; i < count; i++) {
                var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
                item1.Id = i + 1;
                item1.Int32Field = 1;
                item1.DoubleField = 0.1;
                item1.VarcharField = "level1";
                item1.DateTimeField = GetNow();
                item1.EnumInt32Field = EnumInt32Type.Positive1;
                list.Add(item1);
            }
            var retInsert = await context.BatchInsertAsync(list, 2, 38, CancellationToken.None);
            Assert.Equal(38, retInsert);
            var ac = await context.Query<TeBaseFieldNoIdentity>().ToListAsync(CancellationToken.None);
            var ex = list.Skip(2).Take(38).ToList();
            AssertExtend.StrictEqual(ex, ac);

        }

        [Fact]
        public void TestCase_Exists()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
            item1.Id = 0;
            item1.Int32Field = 1;
            item1.DoubleField = 0.1;
            item1.VarcharField = "level1";
            item1.DateTimeField = GetNow();
            item1.EnumInt32Field = EnumInt32Type.Positive1;
            var retInsert = context.Insert(item1);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retInsert);
            var ac = context.Exists<TeBaseFieldNoIdentity>(item1.Id);
            Assert.True(ac);

            var retDelete = context.Delete(item1);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retDelete);
            var ac1 = context.Exists<TeBaseFieldNoIdentity>(item1.Id);
            Assert.False(ac1);
        }


        [Fact]
        public async void TestCase_ExistsAsync()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
            item1.Id = 0;
            item1.Int32Field = 1;
            item1.DoubleField = 0.1;
            item1.VarcharField = "level1";
            item1.DateTimeField = GetNow();
            item1.EnumInt32Field = EnumInt32Type.Positive1;
            var retInsert = await context.InsertAsync(item1, CancellationToken.None);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retInsert);
            var ac = await context.ExistsAsync<TeBaseFieldNoIdentity>(item1.Id, CancellationToken.None);
            Assert.True(ac);

            var retDelete = await context.DeleteAsync(item1, CancellationToken.None);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retDelete);
            var ac1 = await context.ExistsAsync<TeBaseFieldNoIdentity>(item1.Id, CancellationToken.None);
            Assert.False(ac1);
        }

        [Fact]
        public void TestCase_InsertOrUpdate_Single()
        {
            context.TruncateTable<TeBaseField>();
            var item1 = CreateBaseFieldTableList(1)[0];
            var retInsert = context.InsertOrUpdate(item1);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = context.SelectById<TeBaseField>(item1.Id);
            AssertExtend.StrictEqual(item1, item2);
            item1.DateTimeField = GetNow();
            item1.DateTimeFieldNull = null;
            item1.Int32Field = 2;
            item1.Int32FieldNull = null;
            item1.DoubleField = 2.0d;
            item1.DoubleFieldNull = null;
            item1.VarcharField = "abc";
            item1.VarcharFieldNull = null;
            item1.EnumInt32Field = EnumInt32Type.Zero;
            item1.EnumInt32FieldNull = null;
            item1.EnumInt64Field = EnumInt64Type.Zero;
            item1.EnumInt64FieldNull = null;
            var retUpdate = context.InsertOrUpdate(item1);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retUpdate);
            var item3 = context.SelectById<TeBaseField>(item1.Id);
            AssertExtend.StrictEqual(item1, item3);
            var retDelete = context.Delete(item1);
            Assert.Equal(1, retDelete);
            var item4 = context.SelectById<TeBaseField>(item1.Id);
            Assert.Null(item4);
        }

        [Fact]
        public async Task TestCase_InsertOrUpdate_Single_Async()
        {
            context.TruncateTable<TeBaseField>();
            var item1 = CreateBaseFieldTableList(1)[0];
            var retInsert = await context.InsertOrUpdateAsync(item1, CancellationToken.None);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = await context.SelectByIdAsync<TeBaseField>(item1.Id, CancellationToken.None);
            AssertExtend.StrictEqual(item1, item2);
            item1.DateTimeField = GetNow();
            item1.DateTimeFieldNull = null;
            item1.Int32Field = 2;
            item1.Int32FieldNull = null;
            item1.DoubleField = 2.0d;
            item1.DoubleFieldNull = null;
            item1.VarcharField = "abc";
            item1.VarcharFieldNull = null;
            item1.EnumInt32Field = EnumInt32Type.Zero;
            item1.EnumInt32FieldNull = null;
            item1.EnumInt64Field = EnumInt64Type.Zero;
            item1.EnumInt64FieldNull = null;
            var retUpdate = await context.InsertOrUpdateAsync(item1, CancellationToken.None);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retUpdate);
            var item3 = await context.SelectByIdAsync<TeBaseField>(item1.Id, CancellationToken.None);
            AssertExtend.StrictEqual(item1, item3);
            var retDelete = await context.DeleteAsync(item1, CancellationToken.None);
            Assert.Equal(1, retDelete);
            var item4 = await context.SelectByIdAsync<TeBaseField>(item1.Id, CancellationToken.None);
            Assert.Null(item4);
        }

        [Fact]
        public void TestCase_Insert_Reapid_Bulk()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            List<TeBaseField> listAc;
            context.TruncateTable<TeBaseField>();
            var retInsert = context.BatchInsert(listEx, false, false);
            Assert.Equal(count, retInsert);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(listEx.Count, listAc.Count);
            for (int i = 0; i < listAc.Count; i++) {
                Assert.Equal(0, listEx[i].Id);
                Assert.Equal(i + 1, listAc[i].Id);
                Assert.Equal(listEx[i].Int32Field, listAc[i].Int32Field);
            }
        }

        [Fact]
        public async Task TestCase_Insert_Reapid_Bulk_Async()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            List<TeBaseField> listAc;
            context.TruncateTable<TeBaseField>();
            var retInsert = await context.BatchInsertAsync(listEx, false, false, CancellationToken.None);
            Assert.Equal(count, retInsert);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(listEx.Count, listAc.Count);
            for (int i = 0; i < listAc.Count; i++) {
                Assert.Equal(0, listEx[i].Id);
                Assert.Equal(i + 1, listAc[i].Id);
                Assert.Equal(listEx[i].Int32Field, listAc[i].Int32Field);
            }
        }

        [Fact]
        public void TestCase_CUD_Bulk()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            List<TeBaseField> listAc;
            context.TruncateTable<TeBaseField>();
            var retInsert = context.BatchInsert(listEx);
            Assert.Equal(count, retInsert);
            listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.Equal(listEx, listAc);
            DateTime d = GetNow();
            listEx.ForEach(x =>
            {
                x.DateTimeField = d;
                x.DateTimeFieldNull = null;
                x.Int32Field = 2;
                x.Int32FieldNull = null;
                x.DoubleField = 2.0d;
                x.DoubleFieldNull = null;
                x.VarcharField = "abc";
                x.VarcharFieldNull = null;
                x.EnumInt32Field = EnumInt32Type.Zero;
                x.EnumInt32FieldNull = null;
                x.EnumInt64Field = EnumInt64Type.Zero;
                x.EnumInt64FieldNull = null;
            });
            var retUpdate = context.BatchUpdate(listEx);
            Assert.Equal(count, retUpdate);
            listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.Equal(listEx, listAc);
            var retDelete = context.BatchDelete(listEx);
            Assert.Equal(count, retDelete);
            listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.Equal(0, listAc.Count);
        }

        [Fact]
        public async Task TestCase_CUD_Bulk_Async()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            List<TeBaseField> listAc;
            context.TruncateTable<TeBaseField>();
            var retInsert = await context.BatchInsertAsync(listEx, CancellationToken.None);
            Assert.Equal(count, retInsert);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(listEx, listAc);
            DateTime d = GetNow();
            listEx.ForEach(x =>
            {
                x.DateTimeField = d;
                x.DateTimeFieldNull = null;
                x.Int32Field = 2;
                x.Int32FieldNull = null;
                x.DoubleField = 2.0d;
                x.DoubleFieldNull = null;
                x.VarcharField = "abc";
                x.VarcharFieldNull = null;
                x.EnumInt32Field = EnumInt32Type.Zero;
                x.EnumInt32FieldNull = null;
                x.EnumInt64Field = EnumInt64Type.Zero;
                x.EnumInt64FieldNull = null;
            });
            var retUpdate = await context.BatchUpdateAsync(listEx, CancellationToken.None);
            Assert.Equal(count, retUpdate);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(listEx, listAc);
            var retDelete = await context.BatchDeleteAsync(listEx, CancellationToken.None);
            Assert.Equal(count, retDelete);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(0, listAc.Count);
        }

        [Fact]
        public void TestCase_CUD_Bulk2()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            List<TeBaseField> listAc;
            DataContext context = CreateBuilderContextByDi();
            context.TruncateTable<TeBaseField>();
            var retInsert = context.BatchInsert(listEx);
            Assert.Equal(count, retInsert);
            listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.Equal(listEx, listAc);
            DateTime d = GetNow();
            listEx.ForEach(x =>
            {
                x.DateTimeField = d;
                x.DateTimeFieldNull = null;
                x.Int32Field = 2;
                x.Int32FieldNull = null;
                x.DoubleField = 2.0d;
                x.DoubleFieldNull = null;
                x.VarcharField = "abc";
                x.VarcharFieldNull = null;
                x.EnumInt32Field = EnumInt32Type.Zero;
                x.EnumInt32FieldNull = null;
                x.EnumInt64Field = EnumInt64Type.Zero;
                x.EnumInt64FieldNull = null;
            });
            var retUpdate = context.BatchUpdate(listEx);
            Assert.Equal(count, retUpdate);
            listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.Equal(listEx, listAc);
            var retDelete = context.BatchDelete(listEx);
            Assert.Equal(count, retDelete);
            listAc = context.Query<TeBaseField>().ToList();
            AssertExtend.Equal(0, listAc.Count);
        }

        [Fact]
        public async Task TestCase_CUD_Bulk2_Async()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            List<TeBaseField> listAc;
            DataContext context = CreateBuilderContextByDi();
            context.TruncateTable<TeBaseField>();
            var retInsert = await context.BatchInsertAsync(listEx, CancellationToken.None);
            Assert.Equal(count, retInsert);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(listEx, listAc);
            DateTime d = GetNow();
            listEx.ForEach(x =>
            {
                x.DateTimeField = d;
                x.DateTimeFieldNull = null;
                x.Int32Field = 2;
                x.Int32FieldNull = null;
                x.DoubleField = 2.0d;
                x.DoubleFieldNull = null;
                x.VarcharField = "abc";
                x.VarcharFieldNull = null;
                x.EnumInt32Field = EnumInt32Type.Zero;
                x.EnumInt32FieldNull = null;
                x.EnumInt64Field = EnumInt64Type.Zero;
                x.EnumInt64FieldNull = null;
            });
            var retUpdate = await context.BatchUpdateAsync(listEx, CancellationToken.None);
            Assert.Equal(count, retUpdate);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(listEx, listAc);
            var retDelete = await context.BatchDeleteAsync(listEx, CancellationToken.None);
            Assert.Equal(count, retDelete);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(0, listAc.Count);
        }

        [Fact]
        public void TestCase_UpdateMass()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            int result;
            List<TeBaseField> listAc;
            context.TruncateTable<TeBaseField>();
            result = context.BatchInsert(listEx);
            Assert.Equal(count, result);
            DateTime uptime = GetNow();
            result = context.Query<TeBaseField>()
                            .Update(x => new TeBaseField {
                                DateTimeField = uptime,
                                Int32Field = 2,
                                Int32FieldNull = null
                            });
            Assert.Equal(count, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(count, listAc.Count);
            Assert.True(listAc.TrueForAll(x => x.DateTimeField == uptime && x.Int32Field == 2 && x.Int32FieldNull == null));

            const int rdd = 20;

            result = context.Query<TeBaseField>().Where(x => x.Id >= listEx[0].Id && x.Id <= listEx[0].Id + rdd - 1)
                            .Update(x => new TeBaseField {
                                Int32Field = 3
                            });

            Assert.Equal(rdd, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(count, listAc.Count);
            Assert.True(listAc.TrueForAll(x =>
            {
                if (x.Id <= rdd) {
                    return x.Int32Field == 3;
                }
                else {
                    return true;
                }
            }));

            result = context.Query<TeBaseField>().Where(x => x.Id >= listEx[0].Id && x.Id <= listEx[0].Id + rdd - 1)
                .Update(x => new TeBaseField {
                    Int32Field = 6,
                    VarcharField = "66"
                });
            Assert.Equal(rdd, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(count, listAc.Count);
            Assert.True(listAc.TrueForAll(x =>
            {
                if (x.Id <= rdd) {
                    return x.Int32Field == 6 && x.VarcharField == "66";
                }
                else {
                    return true;
                }
            }));
        }

        [Fact]
        public async Task TestCase_UpdateMass_Async()
        {
            const int count = 33;
            var listEx = CreateBaseFieldTableList(count);
            int result;
            List<TeBaseField> listAc;
            context.TruncateTable<TeBaseField>();
            result = await context.BatchInsertAsync(listEx, CancellationToken.None);
            Assert.Equal(count, result);
            DateTime uptime = GetNow();
            result = await context.Query<TeBaseField>()
                            .UpdateAsync(x => new TeBaseField {
                                DateTimeField = uptime,
                                Int32Field = 2,
                                Int32FieldNull = null
                            }, CancellationToken.None);
            Assert.Equal(count, result);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            Assert.Equal(count, listAc.Count);
            Assert.True(listAc.TrueForAll(x => x.DateTimeField == uptime && x.Int32Field == 2 && x.Int32FieldNull == null));

            const int rdd = 20;

            result = await context.Query<TeBaseField>().Where(x => x.Id >= listEx[0].Id && x.Id <= listEx[0].Id + rdd - 1)
                            .UpdateAsync(x => new TeBaseField {
                                Int32Field = 3
                            }, CancellationToken.None);

            Assert.Equal(rdd, result);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            Assert.Equal(count, listAc.Count);
            Assert.True(listAc.TrueForAll(x =>
            {
                if (x.Id <= rdd) {
                    return x.Int32Field == 3;
                }
                else {
                    return true;
                }
            }));

            result = await context.Query<TeBaseField>().Where(x => x.Id >= listEx[0].Id && x.Id <= listEx[0].Id + rdd - 1)
                .UpdateAsync(x => new TeBaseField {
                    Int32Field = 6,
                    VarcharField = "66"
                }, CancellationToken.None);
            Assert.Equal(rdd, result);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            Assert.Equal(count, listAc.Count);
            Assert.True(listAc.TrueForAll(x =>
            {
                if (x.Id <= rdd) {
                    return x.Int32Field == 6 && x.VarcharField == "66";
                }
                else {
                    return true;
                }
            }));
        }

        [Fact]
        public void TestCase_InsertTable()
        {
            const int count = 33;
            int ret = 0;
            var list = CreateAndInsertBaseFieldTableList(count);
            context.TruncateTable<TeBaseFieldSelectInsert>();
            ret = context.Query<TeBaseField>().Insert<TeBaseFieldSelectInsert>();
            Assert.Equal(count, ret);
            List<TeBaseField> ex1 = context.Query<TeBaseField>().ToList();
            List<TeBaseFieldSelectInsert> ac1 = context.Query<TeBaseFieldSelectInsert>().ToList();
            AssertExtend.Equal(ex1, ac1);

            context.TruncateTable<TeBaseFieldSelectInsert>();
            ret = context.Query<TeBaseField>().Where(x => x.Id > 5).Insert<TeBaseFieldSelectInsert>();
            Assert.Equal(count - 5, ret);
            List<TeBaseFieldSelectInsert> ac2 = context.Query<TeBaseFieldSelectInsert>().ToList();
            Assert.Equal(count - 5, ac2.Count);
            for (int i = 1; i <= ac2.Count; i++) {
                Assert.Equal(i, ac2[i - 1].Id);
            }
        }

        [Fact]
        public async Task TestCase_InsertTable_Async()
        {
            const int count = 33;
            int ret = 0;
            var list = CreateAndInsertBaseFieldTableList(count);
            context.TruncateTable<TeBaseFieldSelectInsert>();
            ret = await context.Query<TeBaseField>().InsertAsync<TeBaseFieldSelectInsert>(CancellationToken.None);
            Assert.Equal(count, ret);
            List<TeBaseField> ex1 = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            List<TeBaseFieldSelectInsert> ac1 = await context.Query<TeBaseFieldSelectInsert>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(ex1, ac1);

            context.TruncateTable<TeBaseFieldSelectInsert>();
            ret = await context.Query<TeBaseField>().Where(x => x.Id > 5).InsertAsync<TeBaseFieldSelectInsert>(CancellationToken.None);
            Assert.Equal(count - 5, ret);
            List<TeBaseFieldSelectInsert> ac2 = await context.Query<TeBaseFieldSelectInsert>().ToListAsync(CancellationToken.None);
            Assert.Equal(count - 5, ac2.Count);
            for (int i = 1; i <= ac2.Count; i++) {
                Assert.Equal(i, ac2[i - 1].Id);
            }
        }

        [Fact]
        public void TestCase_InsertTable_NoIdentity()
        {
            const int count = 33;
            int ret = 0;
            var list = CreateAndInsertBaseFieldTableList(count);
            context.TruncateTable<TeBaseFieldSelectInsertNoIdentity>();
            ret = context.Query<TeBaseField>().Insert<TeBaseFieldSelectInsertNoIdentity>();
            Assert.Equal(count, ret);
            var ex1 = context.Query<TeBaseField>().ToList();
            var ac1 = context.Query<TeBaseFieldSelectInsertNoIdentity>().ToList();
            AssertExtend.Equal(ex1, ac1);

            context.TruncateTable<TeBaseFieldSelectInsertNoIdentity>();
            ret = context.Query<TeBaseField>().Where(x => x.Id > 5).Insert<TeBaseFieldSelectInsertNoIdentity>();
            Assert.Equal(count - 5, ret);
            var ex2 = context.Query<TeBaseField>().Where(x => x.Id > 5).ToList();
            var ac2 = context.Query<TeBaseFieldSelectInsertNoIdentity>().ToList();
            AssertExtend.Equal(ex2, ac2);
        }

        [Fact]
        public async Task TestCase_InsertTable_NoIdentity_Async()
        {
            const int count = 33;
            int ret = 0;
            var list = CreateAndInsertBaseFieldTableList(count);
            context.TruncateTable<TeBaseFieldSelectInsertNoIdentity>();
            ret = await context.Query<TeBaseField>().InsertAsync<TeBaseFieldSelectInsertNoIdentity>(CancellationToken.None);
            Assert.Equal(count, ret);
            var ex1 = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            var ac1 = await context.Query<TeBaseFieldSelectInsertNoIdentity>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(ex1, ac1);

            context.TruncateTable<TeBaseFieldSelectInsertNoIdentity>();
            ret = await context.Query<TeBaseField>().Where(x => x.Id > 5).InsertAsync<TeBaseFieldSelectInsertNoIdentity>(CancellationToken.None);
            Assert.Equal(count - 5, ret);
            var ex2 = await context.Query<TeBaseField>().Where(x => x.Id > 5).ToListAsync(CancellationToken.None);
            var ac2 = await context.Query<TeBaseFieldSelectInsertNoIdentity>().ToListAsync(CancellationToken.None);
            AssertExtend.Equal(ex2, ac2);
        }


        [Fact]
        public void TestCase_DeleteMass1()
        {
            const int count = 33;
            List<TeBaseField> listEx = CreateBaseFieldTableList(count);

            int result;
            List<TeBaseField> listAc;

            context.TruncateTable<TeBaseField>();
            result = context.BatchInsert(listEx);
            Assert.Equal(count, result);

            result = context.Query<TeBaseField>().Delete();
            Assert.Equal(count, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(0, listAc.Count);
        }

        [Fact]
        public void TestCase_DeleteMass2()
        {
            const int count = 33;
            List<TeBaseField> listEx = CreateBaseFieldTableList(count);

            int result;
            List<TeBaseField> listAc;

            context.TruncateTable<TeBaseField>();
            result = context.BatchInsert(listEx);
            Assert.Equal(count, result);

            result = context.Query<TeBaseField>().Delete();
            Assert.Equal(count, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(0, listAc.Count);
        }

        [Fact]
        public void TestCase_DeleteMass3()
        {
            const int count = 33;
            List<TeBaseField> listEx = CreateBaseFieldTableList(count);

            int result;
            List<TeBaseField> listAc;
            const int rdd = 20;

            context.TruncateTable<TeBaseField>();
            result = context.BatchInsert(listEx);
            Assert.Equal(count, result);

            result = context.Query<TeBaseField>().Where(x => x.Id >= listEx[0].Id && x.Id <= listEx[0].Id + rdd - 1).Delete();
            Assert.Equal(rdd, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(count - rdd, listAc.Count);
        }

        [Fact]
        public void TestCase_DeleteMass4()
        {
            const int count = 33;
            List<TeBaseField> listEx = CreateBaseFieldTableList(count);

            int result;
            List<TeBaseField> listAc;
            const int rdd = 20;

            context.TruncateTable<TeBaseField>();
            result = context.BatchInsert(listEx);
            Assert.Equal(count, result);

            result = context.Query<TeBaseField>().Where(x => x.Id >= listEx[0].Id && x.Id <= listEx[0].Id + rdd - 1).Delete();
            Assert.Equal(rdd, result);
            listAc = context.Query<TeBaseField>().ToList();
            Assert.Equal(count - rdd, listAc.Count);
        }

        [Fact]
        public async Task TestCase_DeleteMass_Async()
        {
            const int count = 33;
            List<TeBaseField> listEx = CreateBaseFieldTableList(count);

            int result;
            List<TeBaseField> listAc;

            context.TruncateTable<TeBaseField>();
            result = await context.BatchInsertAsync(listEx, CancellationToken.None);
            Assert.Equal(count, result);

            result = await context.Query<TeBaseField>().DeleteAsync(CancellationToken.None);
            Assert.Equal(count, result);
            listAc = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            Assert.Equal(0, listAc.Count);
        }

        [Fact]
        public void TestCase_Trans_Insert()
        {
            List<TeBaseField> list = CreateBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            context.TruncateTable<TeBaseField>();

            ex = list[0];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Insert(ex);
                context.CommitTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);

            ex = list[1];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Insert(ex);
                context.RollbackTrans();
            }
            Assert.True(ex.Id > 0);
            ac = context.SelectById<TeBaseField>(ex.Id);
            Assert.Null(ac);

            ex = list[2];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Insert(ex);
            }
            Assert.True(ex.Id > 0);
            ac = context.SelectById<TeBaseField>(ex.Id);
            Assert.Null(ac);
        }

        [Fact]
        public void TestCase_Trans_Update()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            TeBaseField tg = null;

            ex = list[0];
            ex.Int32Field = 10000;
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Update(ex);
                context.CommitTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);

            ex = list[1];
            tg = context.SelectById<TeBaseField>(ex.Id);
            tg.Int32Field = 10000;
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Update(tg);
                context.RollbackTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);

            ex = list[1];
            tg = context.SelectById<TeBaseField>(ex.Id);
            tg.Int32Field = 10000;
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Update(tg);
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);

        }

        [Fact]
        public void TestCase_Trans_Delete()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            DataContext transContext = null;

            ex = list[0];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Delete(ex);
                context.CommitTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            Assert.Null(ac);

            transContext = CreateContext();
            ex = list[1];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Delete(ex);
                context.RollbackTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);

            transContext = CreateContext();
            ex = list[1];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Delete(ex);
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);
        }

        [Fact]
        public void TestCase_Trans_Exception()
        {
            List<TeBaseField> list = CreateBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            context.TruncateTable<TeBaseField>();

            Assert.Throws<LightDataException>(() =>
            {
                using (var trans = context.BeginTrans()) {
                    context.BeginTrans();

                }
            });

            Assert.Throws<LightDataException>(() =>
            {
                context.CommitTrans();
            });

            Assert.Throws<LightDataException>(() =>
            {
                context.RollbackTrans();
            });

            BaseErrorTable error = new BaseErrorTable();


            ex = list[1];

            Assert.ThrowsAny<Exception>(() =>
            {
                using (var trans = context.BeginTrans()) {
                    //trans.BeginTrans();
                    Assert.True(context.IsTransactionMode);
                    context.Insert(ex);
                    context.Insert(error);
                }
            });


            Assert.False(context.IsTransactionMode);

            Assert.True(ex.Id > 0);
            ac = context.SelectById<TeBaseField>(ex.Id);
            Assert.Null(ac);


        }

        [Fact]
        public void TestCase_Trans_Multi()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            TeBaseField ex1 = null;
            TeBaseField ac1 = null;

            ex = list[0];
            ex.Id = 0;
            ex1 = list[1];
            ex1.Int32Field = 3000;
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Insert(ex);
                context.Update(ex1);
                context.CommitTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);
            ac1 = context.SelectById<TeBaseField>(ex1.Id);
            AssertExtend.StrictEqual(ex1, ac1);
        }

        [Fact]
        public void TestCase_Trans_Repeat()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            TeBaseField ex1 = null;
            TeBaseField ac1 = null;

            ex = list[0];
            ex.Id = 0;
            ex1 = list[1];
            ex1.Int32Field = 3000;
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Insert(ex);
                context.Delete(list[2]);
                context.CommitTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);
            ac = context.SelectById<TeBaseField>(list[2].Id);
            Assert.Null(ac);
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Update(ex1);
                context.Delete(list[3]);
                context.CommitTrans();
            }
            ac1 = context.SelectById<TeBaseField>(ex1.Id);
            AssertExtend.StrictEqual(ex1, ac1);
            ac1 = context.SelectById<TeBaseField>(list[3].Id);
            Assert.Null(ac1);
        }

        [Fact]
        public void TestCase_Trans_Bulk()
        {
            var list = CreateBaseFieldTableList(10);
            List<TeBaseField> ex = null;
            List<TeBaseField> ac = null;

            context.TruncateTable<TeBaseField>();
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.Insert(list[0]);
                context.BatchInsert(list, 1, list.Count - 1);
                context.CommitTrans();
            }

            ex = list;
            ac = context.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(ex, ac);

            context.TruncateTable<TeBaseField>();
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.BatchInsert(list, 0, list.Count - 1);
                context.Insert(list[list.Count - 1]);
                context.CommitTrans();
            }

            ex = list;
            ac = context.Query<TeBaseField>().ToList();
            AssertExtend.StrictEqual(ex, ac);
        }

        [Fact]
        public void TestCase_Trans_SafeLevel()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;

            DataContext context1 = CreateContext();
            using (var trans = context.BeginTrans(SafeLevel.Normal)) {
                //trans.BeginTrans(SafeLevel.Normal);
                ex = context.SelectById<TeBaseField>(list[0].Id);
                Task.Run(() =>
                {
                    ac = context1.SelectById<TeBaseField>(list[0].Id);
                });
                System.Threading.Thread.Sleep(500);
                ex.Int32Field = 1000;
                context.Update(ex);
                context.CommitTrans();
            }
            AssertExtend.StrictEqual(list[0], ac);

            using (var trans = context.BeginTrans(SafeLevel.High)) {
                //trans.BeginTrans(SafeLevel.High);
                ex = context.SelectById<TeBaseField>(list[0].Id);
                Task.Run(() =>
                {
                    ac = context1.SelectById<TeBaseField>(list[0].Id);
                });
                System.Threading.Thread.Sleep(500);
                ex.Int32Field = 1000;
                context.Update(ex);
                context.CommitTrans();
            }
            AssertExtend.StrictEqual(ex, ac);


        }

        [Fact]
        public void TestCase_Trans_SafeLevel_2()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            int ex = 0;
            int ac = 0;

            DataContext context1 = CreateContext();
            using (var trans = context.BeginTrans(SafeLevel.Normal)) {
                //trans.BeginTrans(SafeLevel.Normal);
                ex = context.Query<TeBaseField>().Where(x => x.Id > 5).Count();
                Task.Run(() =>
                {
                    context1.Insert(list[0]);
                });
                System.Threading.Thread.Sleep(500);
                ac = context.Query<TeBaseField>().Where(x => x.Id > 5).Count();
                context.CommitTrans();
            }
            System.Threading.Thread.Sleep(500);
            Assert.Equal(ex + 1, ac);

            using (var trans = context.BeginTrans(SafeLevel.High)) {
                //trans.BeginTrans(SafeLevel.High);
                ex = context.Query<TeBaseField>().Where(x => x.Id > 5).Count();
                Task.Run(() =>
                {
                    context1.Insert(list[0]);
                });
                System.Threading.Thread.Sleep(500);
                ac = context.Query<TeBaseField>().Where(x => x.Id > 5).Count();
                context.CommitTrans();
            }
            System.Threading.Thread.Sleep(500);
            Assert.Equal(ex, ac);

            using (var trans = context.BeginTrans(SafeLevel.Serializable)) {
                //trans.BeginTrans(SafeLevel.Serializable);
                ex = context.Query<TeBaseField>().Where(x => x.Id > 5).Count();
                Task.Run(() =>
                {
                    context1.Insert(list[0]);
                });
                System.Threading.Thread.Sleep(500);
                ac = context.Query<TeBaseField>().Where(x => x.Id > 5).Count();
                context.CommitTrans();
            }
            System.Threading.Thread.Sleep(500);
            Assert.Equal(ex, ac);
        }

        [Fact]
        public void TestCase_Trans_SafeLevel_3()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;

            DataContext context1 = CreateContext();
            using (var trans = context.BeginTrans(SafeLevel.Normal)) {
                //trans.BeginTrans(SafeLevel.Normal);
                ex = context.SelectById<TeBaseField>(list[0].Id);
                Task.Run(() =>
                {
                    context1.Delete(list[0]);
                });
                System.Threading.Thread.Sleep(500);
                ac = context.SelectById<TeBaseField>(list[0].Id);
                context.CommitTrans();
            }
            System.Threading.Thread.Sleep(500);
            Assert.Null(ac);
            list.Remove(list[0]);

            using (var trans = context.BeginTrans(SafeLevel.Normal)) {
                //trans.BeginTrans(SafeLevel.Normal);
                ex = context.SelectById<TeBaseField>(list[0].Id);
                Task.Run(() =>
                {
                    context1.Delete(list[0]);
                });
                System.Threading.Thread.Sleep(500);
                ac = context.SelectById<TeBaseField>(list[0].Id);
                context.CommitTrans();
            }
            System.Threading.Thread.Sleep(500);
            Assert.Null(ac);
            list.Remove(list[0]);

            using (var trans = context.BeginTrans(SafeLevel.Serializable)) {
                //trans.BeginTrans(SafeLevel.Serializable);
                ex = context.SelectById<TeBaseField>(list[0].Id);
                Task.Run(() =>
                {
                    context1.Delete(list[0]);
                });
                System.Threading.Thread.Sleep(500);
                ac = context.SelectById<TeBaseField>(list[0].Id);
                context.CommitTrans();
            }
            System.Threading.Thread.Sleep(500);
            AssertExtend.StrictEqual(ex, ac);
            list.Remove(list[0]);

        }

        [Fact]
        public async Task TestCase_Trans_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            TeBaseField ex1 = null;
            TeBaseField ac1 = null;

            ex = list[0];
            ex.Id = 0;
            ex1 = list[1];
            ex1.Int32Field = 3000;
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                await context.InsertAsync(ex, CancellationToken.None);
                await context.UpdateAsync(ex1, CancellationToken.None);
                context.CommitTrans();
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);
            ac1 = context.SelectById<TeBaseField>(ex1.Id);
            AssertExtend.StrictEqual(ex1, ac1);
        }

        [Fact]
        public async Task TestCase_Trans_Bulk_Async()
        {
            var list = CreateBaseFieldTableList(10);
            List<TeBaseField> ex = null;
            List<TeBaseField> ac = null;

            context.TruncateTable<TeBaseField>();
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                await context.InsertAsync(list[0], CancellationToken.None);
                await context.BatchInsertAsync(list, 1, list.Count - 1, CancellationToken.None);
                context.CommitTrans();
            }

            ex = list;
            ac = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.StrictEqual(ex, ac);

            context.TruncateTable<TeBaseField>();
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                await context.BatchInsertAsync(list, 0, list.Count - 1, CancellationToken.None);
                await context.InsertAsync(list[list.Count - 1], CancellationToken.None);
                context.CommitTrans();
            }

            ex = list;
            ac = await context.Query<TeBaseField>().ToListAsync(CancellationToken.None);
            AssertExtend.StrictEqual(ex, ac);
        }
        #endregion

        [Fact]
        public void TestCase_SqlString_QueryList()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = executor.QueryList<TeBaseField>();
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = executor.QueryList<TeBaseField>(5, 3);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">5 and \"Id\"<=8 Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = executor.QueryList<TeBaseField>();
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">:p1 and \"Id\"<=:p2 Order By \"Id\"";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateSqlStringExecutor(sql, ps);
            listAc = executor.QueryList<TeBaseField>();
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql);
                listAc = executor.QueryList<TeBaseField>();
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\" limit 1";
            executor = context.CreateSqlStringExecutor(sql);
            var itemAc = executor.QueryFirst<TeBaseField>();
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public async Task TestCase_SqlString_QueryListAsync()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = await executor.QueryListAsync<TeBaseField>(5, 3, CancellationToken.None);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">5 and \"Id\"<=8 Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">:p1 and \"Id\"<=:p2 Order By \"Id\"";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateSqlStringExecutor(sql, ps);
            listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql);
                listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\" limit 1";
            executor = context.CreateSqlStringExecutor(sql);
            var itemAc = await executor.QueryFirstAsync<TeBaseField>(CancellationToken.None);
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public void TestCase_SqlString_Query()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = new List<TeBaseField>();
            listAc.AddRange(executor.Query<TeBaseField>());
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = new List<TeBaseField>(executor.QueryList<TeBaseField>(5, 3));
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">5 and \"Id\"<=8 Order By \"Id\"";
            executor = context.CreateSqlStringExecutor(sql);
            listAc = new List<TeBaseField>(executor.QueryList<TeBaseField>());
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">:p1 and \"Id\"<=:p2 Order By \"Id\"";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateSqlStringExecutor(sql, ps);
            listAc = new List<TeBaseField>(executor.QueryList<TeBaseField>());
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql);
                listAc = new List<TeBaseField>(executor.Query<TeBaseField>());
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);
        }

        [Fact]
        public void TestCase_SqlString_Execute()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ret;
            TeBaseField itemEx;
            TeBaseField itemAc;
            SqlExecutor executor;
            DataParameter[] ps;

            itemEx = list[0];
            sql = "update \"Te_BaseField\" set \"VarcharField\"='abc' where \"Id\"=" + itemEx.Id;
            executor = context.CreateSqlStringExecutor(sql);
            ret = executor.ExecuteNonQuery();
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "abc";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[1];
            sql = "update \"Te_BaseField\" set \"VarcharField\"=:p2 where \"Id\"=:p1";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", itemEx.Id);
            ps[1] = new DataParameter("p2", "bcd");
            executor = context.CreateSqlStringExecutor(sql, ps);
            ret = executor.ExecuteNonQuery();
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "bcd";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[2];
            sql = "update \"Te_BaseField\" set \"VarcharField\"=:p2 where \"Id\"=:p1";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", itemEx.Id);
            ps[1] = new DataParameter("p2", "cdf");
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql, ps);
                ret = executor.ExecuteNonQuery();
                Assert.Equal(1, ret);
                context.CommitTrans();
            }
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "cdf";
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public async Task TestCase_SqlString_ExecuteAsync()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ret;
            TeBaseField itemEx;
            TeBaseField itemAc;
            SqlExecutor executor;
            DataParameter[] ps;

            itemEx = list[0];
            sql = "update \"Te_BaseField\" set \"VarcharField\"='abc' where \"Id\"=" + itemEx.Id;
            executor = context.CreateSqlStringExecutor(sql);
            ret = await executor.ExecuteNonQueryAsync(CancellationToken.None);
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "abc";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[1];
            sql = "update \"Te_BaseField\" set \"VarcharField\"=:p2 where \"Id\"=:p1";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", itemEx.Id);
            ps[1] = new DataParameter("p2", "bcd");
            executor = context.CreateSqlStringExecutor(sql, ps);
            ret = await executor.ExecuteNonQueryAsync(CancellationToken.None);
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "bcd";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[2];
            sql = "update \"Te_BaseField\" set \"VarcharField\"=:p2 where \"Id\"=:p1";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", itemEx.Id);
            ps[1] = new DataParameter("p2", "cdf");
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql, ps);
                ret = await executor.ExecuteNonQueryAsync(CancellationToken.None);
                Assert.Equal(1, ret);
                context.CommitTrans();
            }
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "cdf";
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public void TestCase_SqlString_ExecuteScalar()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;
            int ac;

            sql = "select count(1) from \"Te_BaseField\"";
            executor = context.CreateSqlStringExecutor(sql);
            ac = Convert.ToInt32(executor.ExecuteScalar());
            Assert.Equal(list.Count, ac);

            sql = "select count(1) from \"Te_BaseField\" where \"Id\"<=:p1";
            ps = new DataParameter[1];
            ps[0] = new DataParameter("p1", 5);
            executor = context.CreateSqlStringExecutor(sql, ps);
            ac = Convert.ToInt32(executor.ExecuteScalar());
            Assert.Equal(5, ac);

            sql = "select count(1) from \"Te_BaseField\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql);
                ac = Convert.ToInt32(executor.ExecuteScalar());
                context.CommitTrans();
            }
            Assert.Equal(list.Count, ac);
        }

        [Fact]
        public async Task TestCase_SqlString_ExecuteScalarAsync()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;
            int ac;

            sql = "select count(1) from \"Te_BaseField\"";
            executor = context.CreateSqlStringExecutor(sql);
            ac = Convert.ToInt32(await executor.ExecuteScalarAsync(CancellationToken.None));
            Assert.Equal(list.Count, ac);

            sql = "select count(1) from \"Te_BaseField\" where \"Id\"<=:p1";
            ps = new DataParameter[1];
            ps[0] = new DataParameter("p1", 5);
            executor = context.CreateSqlStringExecutor(sql, ps);
            ac = Convert.ToInt32(await executor.ExecuteScalarAsync(CancellationToken.None));
            Assert.Equal(5, ac);

            sql = "select count(1) from \"Te_BaseField\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateSqlStringExecutor(sql);
                ac = Convert.ToInt32(await executor.ExecuteScalarAsync(CancellationToken.None));
                context.CommitTrans();
            }
            Assert.Equal(list.Count, ac);
        }

        [Fact]
        public void TestCase_StoreProcedure_Execute_OutParameter()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest7";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 0, DataParameterMode.Output);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            executor.ExecuteNonQuery();
            Assert.Equal(5, Convert.ToInt32(ps[1].Value));

            sql = "sptest7";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 0, DataParameterMode.Output);
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql, ps);
                executor.ExecuteNonQuery();
                context.CommitTrans();
            }
            Assert.Equal(5, Convert.ToInt32(ps[1].Value));
        }

        [Fact]
        public async Task TestCase_StoreProcedure_Execute_OutParameterAsync()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest7";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 0, DataParameterMode.Output);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            await executor.ExecuteNonQueryAsync(CancellationToken.None);
            Assert.Equal(5, Convert.ToInt32(ps[1].Value));

            sql = "sptest7";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 0, DataParameterMode.Output);
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql, ps);
                await executor.ExecuteNonQueryAsync(CancellationToken.None);
                context.CommitTrans();
            }
            Assert.Equal(5, Convert.ToInt32(ps[1].Value));
        }

        [Fact]
        public void TestCase_StoreProcedure_QueryList()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            listAc = executor.QueryList<TeBaseField>();
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            listAc = executor.QueryList<TeBaseField>(5, 3);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest2";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            listAc = executor.QueryList<TeBaseField>();
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql);
                listAc = executor.QueryList<TeBaseField>();
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            var itemAc = executor.QueryFirst<TeBaseField>();
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_QueryListAsync()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            listAc = await executor.QueryListAsync<TeBaseField>(5, 3, CancellationToken.None);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest2";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql);
                listAc = await executor.QueryListAsync<TeBaseField>(CancellationToken.None);
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            var itemAc = await executor.QueryFirstAsync<TeBaseField>(CancellationToken.None);
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public void TestCase_StoreProcedure_Query()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            listAc = new List<TeBaseField>();
            listAc.AddRange(executor.Query<TeBaseField>());
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            listAc = new List<TeBaseField>(executor.QueryList<TeBaseField>(5, 3));
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest2";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            listAc = new List<TeBaseField>(executor.QueryList<TeBaseField>());
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql);
                listAc = new List<TeBaseField>(executor.QueryList<TeBaseField>());
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);
        }

        [Fact]
        public void TestCase_StoreProcedure_Execute()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            TeBaseField user;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest3";
            executor = context.CreateStoreProcedureExecutor(sql);
            executor.ExecuteNonQuery();
            user = context.SelectById<TeBaseField>(1);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);

            sql = "sptest4";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 2);
            ps[1] = new DataParameter("p2", "bcd");
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            executor.ExecuteNonQuery();
            user = context.SelectById<TeBaseField>(2);
            Assert.NotNull(user);
            Assert.Equal("bcd", user.VarcharField);

            sql = "sptest4";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 3);
            ps[1] = new DataParameter("p2", "abc");
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql, ps);
                executor.ExecuteNonQuery();
                context.CommitTrans();
            }
            user = context.SelectById<TeBaseField>(3);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_ExecuteAsync()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            TeBaseField user;
            SqlExecutor executor;
            DataParameter[] ps;

            sql = "sptest3";
            executor = context.CreateStoreProcedureExecutor(sql);
            await executor.ExecuteNonQueryAsync(CancellationToken.None);
            user = context.SelectById<TeBaseField>(1);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);

            sql = "sptest4";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 2);
            ps[1] = new DataParameter("p2", "bcd");
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            await executor.ExecuteNonQueryAsync(CancellationToken.None);
            user = context.SelectById<TeBaseField>(2);
            Assert.NotNull(user);
            Assert.Equal("bcd", user.VarcharField);

            sql = "sptest4";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 3);
            ps[1] = new DataParameter("p2", "abc");
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql, ps);
                await executor.ExecuteNonQueryAsync(CancellationToken.None);
                context.CommitTrans();
            }
            user = context.SelectById<TeBaseField>(3);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);
        }

        [Fact]
        public void TestCase_StoreProcedure_ExecuteScalar()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;
            int ac;

            sql = "sptest5";
            executor = context.CreateStoreProcedureExecutor(sql);
            ac = Convert.ToInt32(executor.ExecuteScalar());
            Assert.Equal(10, ac);

            sql = "sptest6";
            ps = new DataParameter[1];
            ps[0] = new DataParameter("p1", 5);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            ac = Convert.ToInt32(executor.ExecuteScalar());
            Assert.Equal(5, ac);

            sql = "sptest5";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql);
                ac = Convert.ToInt32(executor.ExecuteScalar());
                context.CommitTrans();
            }
            Assert.Equal(10, ac);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_ExecuteScalarAsync()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;
            int ac;

            sql = "sptest5";
            executor = context.CreateStoreProcedureExecutor(sql);
            ac = Convert.ToInt32(await executor.ExecuteScalarAsync(CancellationToken.None));
            Assert.Equal(10, ac);

            sql = "sptest6";
            ps = new DataParameter[1];
            ps[0] = new DataParameter("p1", 5);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            ac = Convert.ToInt32(await executor.ExecuteScalarAsync(CancellationToken.None));
            Assert.Equal(5, ac);

            sql = "sptest5";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                executor = context.CreateStoreProcedureExecutor(sql);
                ac = Convert.ToInt32(await executor.ExecuteScalarAsync(CancellationToken.None));
                context.CommitTrans();
            }
            Assert.Equal(10, ac);
        }

        [Fact]
        public void BaseTest_AliasTable()
        {
            List<TeBaseField> list = CreateBaseFieldTableList(10);
            context.TruncateTable<TeBaseField>();
            context.TruncateTable<TeBaseFieldAlias>();
            var item = list[0];

            context.SetAliasTableName<TeBaseField>("Te_BaseField_Alias");
            context.Insert(item);
            context.ResetAliasTableName<TeBaseField>();
            var ac0 = context.SelectByKey<TeBaseFieldAlias>(item.Id);
            var ac1 = context.SelectByKey<TeBaseField>(item.Id);

            Assert.Null(ac1);
            AssertExtend.Equal(item, ac0);

            context.SetAliasTableName<TeBaseField>("Te_BaseField_Alias");
            var ac2 = context.SelectByKey<TeBaseField>(item.Id);
            context.ResetAliasTableName<TeBaseField>();
            AssertExtend.Equal(item, ac2);
        }

        [Fact]
        public void TestCase_CUD_Single_S1()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase1>(item.Id);
            item1.Id = 0;
            context.TruncateTable<MyBase1>();
            var retInsert = context.Insert(item1);
            Assert.Equal(1, item1.Id);
            Assert.Equal(1, retInsert);
            var item2 = context.SelectById<MyBase1>(item1.Id);
            AssertExtend.StrictEqual(item1, item2);
            item2.DateTimeField = GetNow();
            item2.DateTimeFieldNull = null;
            item2.Int32Field = 2;
            item2.Int32FieldNull = null;
            item2.DoubleField = 2.0d;
            item2.DoubleFieldNull = null;
            item2.VarcharField = "abc";
            item2.VarcharFieldNull = null;
            item2.EnumInt32Field = EnumInt32Type.Zero;
            item2.EnumInt32FieldNull = null;
            item2.EnumInt64Field = EnumInt64Type.Zero;
            item2.EnumInt64FieldNull = null;
            var retUpdate = context.Update(item2);
            Assert.Equal(1, item2.Id);
            Assert.Equal(1, retUpdate);
            var item3 = context.SelectById<MyBase1>(item1.Id);
            AssertExtend.StrictEqual(item2, item3);
            var retDelete = context.Delete(item3);
            Assert.Equal(1, item3.Id);
            Assert.Equal(1, retDelete);
            var item4 = context.SelectById<MyBase1>(item1.Id);
            Assert.Null(item4);
        }

        [Fact]
        public void TestCase_FunctionControl_ReadOnly()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase3>(item.Id);

            Assert.Equal(item.Int32Field, item1.Int32Field);
            Assert.Equal(item.Int32FieldNull, item1.Int32FieldNull);

            item1.Int32Field = 10000;
            item1.Int32FieldNull = 20000;
            var retUpdate = context.Update(item1);
            Assert.Equal(1, retUpdate);

            var item2 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int32Field, item2.Int32Field);
            Assert.Equal(item.Int32FieldNull, item2.Int32FieldNull);

            var retInsert = context.Insert(item1);
            Assert.Equal(1, retInsert);

            var item3 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int32Field, item3.Int32Field);
            Assert.Null(item3.Int32FieldNull);
        }


        [Fact]
        public void TestCase_FunctionControl_Create()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase3>(item.Id);

            Assert.Equal(item.DecimalField, item1.DecimalField);
            Assert.Equal(item.DecimalFieldNull, item1.DecimalFieldNull);

            item1.DecimalField = 10000;
            item1.DecimalFieldNull = 20000;
            var retUpdate = context.Update(item1);
            Assert.Equal(1, retUpdate);

            var item2 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.DecimalField, item2.DecimalField);
            Assert.Equal(item.DecimalFieldNull, item2.DecimalFieldNull);

            var retInsert = context.Insert(item1);
            Assert.Equal(1, retInsert);

            var item3 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.DecimalField, item3.DecimalField);
            Assert.Equal(item1.DecimalFieldNull, item3.DecimalFieldNull);
        }

        [Fact]
        public void TestCase_FunctionControl_Update()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase3>(item.Id);

            Assert.Equal(item.Int64Field, item1.Int64Field);
            Assert.Equal(item.Int64FieldNull, item1.Int64FieldNull);

            item1.Int64Field = 10000;
            item1.Int64FieldNull = 20000;
            var retUpdate = context.Update(item1);
            Assert.Equal(1, retUpdate);

            var item2 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int64Field, item2.Int64Field);
            Assert.Equal(item1.Int64FieldNull, item2.Int64FieldNull);

            var retInsert = context.Insert(item1);
            Assert.Equal(1, retInsert);

            var item3 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int64Field, item3.Int64Field);
            Assert.Null(item3.Int64FieldNull);
        }

        [Fact]
        public void TestCase_FunctionControl_ReadOnly_Batch()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase3>(item.Id);

            Assert.Equal(item.Int32Field, item1.Int32Field);
            Assert.Equal(item.Int32FieldNull, item1.Int32FieldNull);

            item1.Int32Field = 10000;
            item1.Int32FieldNull = 20000;
            var retUpdate = context.BatchUpdate(new[] { item1 });
            Assert.Equal(1, retUpdate);

            var item2 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int32Field, item2.Int32Field);
            Assert.Equal(item.Int32FieldNull, item2.Int32FieldNull);

            var retInsert = context.BatchInsert(new[] { item1 });
            Assert.Equal(1, retInsert);

            var item3 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int32Field, item3.Int32Field);
            Assert.Null(item3.Int32FieldNull);
        }


        [Fact]
        public void TestCase_FunctionControl_Create_Batch()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase3>(item.Id);

            Assert.Equal(item.DecimalField, item1.DecimalField);
            Assert.Equal(item.DecimalFieldNull, item1.DecimalFieldNull);

            item1.DecimalField = 10000;
            item1.DecimalFieldNull = 20000;
            var retUpdate = context.BatchUpdate(new[] { item1 });
            Assert.Equal(1, retUpdate);

            var item2 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.DecimalField, item2.DecimalField);
            Assert.Equal(item.DecimalFieldNull, item2.DecimalFieldNull);

            var retInsert = context.BatchInsert(new[] { item1 });
            Assert.Equal(1, retInsert);

            var item3 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.DecimalField, item3.DecimalField);
            Assert.Equal(item1.DecimalFieldNull, item3.DecimalFieldNull);
        }

        [Fact]
        public void TestCase_FunctionControl_Update_Batch()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            var item1 = context.SelectById<MyBase3>(item.Id);

            Assert.Equal(item.Int64Field, item1.Int64Field);
            Assert.Equal(item.Int64FieldNull, item1.Int64FieldNull);

            item1.Int64Field = 10000;
            item1.Int64FieldNull = 20000;
            var retUpdate = context.BatchUpdate(new[] { item1 });
            Assert.Equal(1, retUpdate);

            var item2 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int64Field, item2.Int64Field);
            Assert.Equal(item1.Int64FieldNull, item2.Int64FieldNull);

            var retInsert = context.BatchInsert(new[] { item1 });
            Assert.Equal(1, retInsert);

            var item3 = context.SelectById<MyBase3>(item1.Id);

            Assert.Equal(item1.Int64Field, item3.Int64Field);
            Assert.Null(item3.Int64FieldNull);
        }

        [Fact]
        public void TestCase_DbType()
        {
            context.TruncateTable<TeBaseField>();
            var item = CreateAndInsertBaseFieldTableList(1)[0];
            item.VarcharField = "1111111111111111111111111111111111";
            item.VarcharFieldNull = "1111111111111111111111111111111111";
            item.DecimalField = 2312.45345m;
            item.DecimalFieldNull = 2312.45345m;
            context.Update(item);
            var item1 = context.SelectById<MyBase5>(item.Id);

            Assert.Equal(item.VarcharField, item1.VarcharField);
            Assert.Equal(item.VarcharFieldNull, item1.VarcharFieldNull);

            context.Insert(item1);

            var item2 = context.SelectById<MyBase5>(item1.Id);

            Assert.Equal(5, item2.VarcharField.Length);
            Assert.Equal(6, item2.VarcharFieldNull.Length);
            //Assert.Equal(2312.45m, item2.DecimalField);
            //Assert.Equal(2312.4m, item2.DecimalFieldNull);

            context.Insert(item1);

            var item3 = context.SelectById<MyBase5>(item1.Id);

            Assert.Equal(5, item3.VarcharField.Length);
            Assert.Equal(6, item3.VarcharFieldNull.Length);
            //Assert.Equal(2312.45m, item2.DecimalField);
            //Assert.Equal(2312.4m, item2.DecimalFieldNull);
        }

        [Fact]
        public void TestCase_DeleteByKey()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
            item1.Id = 0;
            item1.Int32Field = 1;
            item1.DoubleField = 0.1;
            item1.VarcharField = "level1";
            item1.DateTimeField = GetNow();
            item1.EnumInt32Field = EnumInt32Type.Positive1;
            var retInsert = context.Insert(item1);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retInsert);
            var ac = context.DeleteByKey<TeBaseFieldNoIdentity>(item1.Id);
            Assert.Equal(1, ac);
            var ac1 = context.Exists<TeBaseFieldNoIdentity>(item1.Id);
            Assert.False(ac1);
        }

        [Fact]
        public async void TestCase_DeleteByKey_Async()
        {
            context.TruncateTable<TeBaseFieldNoIdentity>();
            var item1 = context.CreateNew<TeBaseFieldNoIdentity>();
            item1.Id = 0;
            item1.Int32Field = 1;
            item1.DoubleField = 0.1;
            item1.VarcharField = "level1";
            item1.DateTimeField = GetNow();
            item1.EnumInt32Field = EnumInt32Type.Positive1;
            var retInsert = await context.InsertAsync(item1);
            Assert.Equal(0, item1.Id);
            Assert.Equal(1, retInsert);
            var ac = await context.DeleteByKeyAsync<TeBaseFieldNoIdentity>(item1.Id);
            Assert.Equal(1, ac);
            var ac1 = await context.ExistsAsync<TeBaseFieldNoIdentity>(item1.Id);
            Assert.False(ac1);
        }

        [Fact]
        public void TestCase_DataSet_Query()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            SqlExecutor executor;
            DataParameter[] ps;
            List<TeBaseField> listEx;
            DataSet ds;

            sql = "sptest1";
            executor = context.CreateStoreProcedureExecutor(sql);
            ds = executor.QueryDataSet();
            listEx = list;
            Assert.Equal(listEx.Count, ds.Tables[0].Rows.Count);

            sql = "sptest2";
            ps = new DataParameter[2];
            ps[0] = new DataParameter("p1", 5);
            ps[1] = new DataParameter("p2", 8);
            executor = context.CreateStoreProcedureExecutor(sql, ps);
            ds = executor.QueryDataSet();
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            Assert.Equal(listEx.Count, ds.Tables[0].Rows.Count);

            sql = "select * from \"Te_BaseField\"";
            executor = context.CreateSqlStringExecutor(sql);
            ds = executor.QueryDataSet();
            listEx = list;
            Assert.Equal(listEx.Count, ds.Tables[0].Rows.Count);
        }

        [Fact]
        public void TestCase_Trans_Rollback()
        {
            List<TeBaseField> list = CreateBaseFieldTableList(10);
            TeBaseField ex = null;
            TeBaseField ac = null;
            context.TruncateTable<TeBaseField>();

            ex = list[0];
            using (var trans = context.BeginTrans(false)) {
                //trans.BeginTrans();
                Assert.True(trans.CheckTrans());
                context.Insert(ex);
                context.RollbackTrans();
                Assert.Throws<LightDataException>(() =>
                {
                    context.Insert(ex);
                });
                Assert.ThrowsAny<Exception>(() =>
                {
                    context.CommitTrans();
                });
            }
            Assert.Equal(1, ex.Id);
            ac = context.SelectById<TeBaseField>(ex.Id);
            Assert.Null(ac);

            ex = list[1];
            var t = context.BeginTrans(true);
            Assert.True(t.CheckTrans());
            //trans.BeginTrans();
            context.Insert(ex);
            context.RollbackTrans();
            Assert.False(t.CheckTrans());
            ex = list[2];
            context.Insert(ex);
            Assert.Throws<LightDataException>(() =>
            {
                context.CommitTrans();
            });

            Assert.True(ex.Id > 0);
            ac = context.SelectById<TeBaseField>(ex.Id);
            AssertExtend.StrictEqual(ex, ac);

            BaseErrorTable error = new BaseErrorTable();
            ex = list[3];
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                Assert.True(context.IsTransactionMode);
                context.Insert(ex);
                Assert.ThrowsAny<Exception>(() =>
                {
                    context.Insert(error);
                });
                Assert.Throws<LightDataException>(() =>
                {
                    context.CommitTrans();
                });
            }
            ac = context.SelectById<TeBaseField>(ex.Id);
            Assert.Null(ac);
        }

        [Fact]
        public void TestCase_SqlString_QueryList_WithObject()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            listAc = context.QuerySqlList<TeBaseField>(sql);
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            listAc = context.QuerySqlList<TeBaseField>(sql, 5, 3);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">5 and \"Id\"<=8 Order By \"Id\"";
            listAc = context.QuerySqlList<TeBaseField>(sql);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">{from_id} and \"Id\"<={to_id} Order By \"Id\"";
            listAc = context.QuerySqlList<TeBaseField>(sql, new { from_id = 5, to_id = 8 });
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">{range.from_id} and \"Id\"<={range.to_id} Order By \"Id\"";
            listAc = context.QuerySqlList<TeBaseField>(sql, new { range = new { from_id = 5, to_id = 8 } });
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);


            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                listAc = context.QuerySqlList<TeBaseField>(sql);
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\" limit 1";
            var itemAc = context.QuerySqlFirst<TeBaseField>(sql);
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public async Task TestCase_SqlString_QueryList_WithObject_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            listAc = await context.QuerySqlListAsync<TeBaseField>(sql);
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            listAc = await context.QuerySqlListAsync<TeBaseField>(sql, 5, 3);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">5 and \"Id\"<=8 Order By \"Id\"";
            listAc = await context.QuerySqlListAsync<TeBaseField>(sql);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">{from_id} and \"Id\"<={to_id} Order By \"Id\"";
            listAc = await context.QuerySqlListAsync<TeBaseField>(sql, new { from_id = 5, to_id = 8 });
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" where \"Id\">{range.from_id} and \"Id\"<={range.to_id} Order By \"Id\"";
            listAc = await context.QuerySqlListAsync<TeBaseField>(sql, new { range = new { from_id = 5, to_id = 8 } });
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                listAc = await context.QuerySqlListAsync<TeBaseField>(sql);
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "select * from \"Te_BaseField\" Order By \"Id\" limit 1";
            var itemAc = await context.QuerySqlFirstAsync<TeBaseField>(sql);
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public void TestCase_SqlString_Execute_WithObject()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ret;
            TeBaseField itemEx;
            TeBaseField itemAc;

            itemEx = list[0];
            sql = "update \"Te_BaseField\" set \"VarcharField\"='abc' where \"Id\"=" + itemEx.Id;
            ret = context.ExecuteNonQuerySqlString(sql);
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "abc";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[1];
            sql = "update \"Te_BaseField\" set \"VarcharField\"={varchar} where \"Id\"={id}";
            ret = context.ExecuteNonQuerySqlString(sql, new { varchar = "bcd", id = itemEx.Id });
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "bcd";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[2];
            sql = "update \"Te_BaseField\" set \"VarcharField\"={varchar} where \"Id\"={id}";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                ret = context.ExecuteNonQuerySqlString(sql, new { varchar = "cdf", id = itemEx.Id });
                Assert.Equal(1, ret);
                context.CommitTrans();
            }
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "cdf";
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public async Task TestCase_SqlString_Execute_WithObject_Async()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ret;
            TeBaseField itemEx;
            TeBaseField itemAc;

            itemEx = list[0];
            sql = "update \"Te_BaseField\" set \"VarcharField\"='abc' where \"Id\"=" + itemEx.Id;
            ret = await context.ExecuteNonQuerySqlStringAsync(sql);
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "abc";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[1];
            sql = "update \"Te_BaseField\" set \"VarcharField\"={varchar} where \"Id\"={id}";
            ret = await context.ExecuteNonQuerySqlStringAsync(sql, new { varchar = "bcd", id = itemEx.Id });
            Assert.Equal(1, ret);
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "bcd";
            AssertExtend.StrictEqual(itemEx, itemAc);

            itemEx = list[2];
            sql = "update \"Te_BaseField\" set \"VarcharField\"={varchar} where \"Id\"={id}";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                ret = await context.ExecuteNonQuerySqlStringAsync(sql, new { varchar = "cdf", id = itemEx.Id });
                Assert.Equal(1, ret);
                context.CommitTrans();
            }
            itemAc = context.SelectById<TeBaseField>(itemEx.Id);
            itemEx.VarcharField = "cdf";
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public void TestCase_SqlString_ExecuteScalar_WithObject()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ac;

            sql = "select count(1) from \"Te_BaseField\"";
            ac = Convert.ToInt32(context.ExecuteScalarSqlString(sql));
            Assert.Equal(list.Count, ac);

            sql = "select count(1) from \"Te_BaseField\" where \"Id\"<={id}";
            ac = Convert.ToInt32(context.ExecuteScalarSqlString(sql, new { id = 5 }));
            Assert.Equal(5, ac);

            sql = "select count(1) from \"Te_BaseField\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                ac = Convert.ToInt32(context.ExecuteScalarSqlString(sql));
                context.CommitTrans();
            }
            Assert.Equal(list.Count, ac);
        }

        [Fact]
        public async Task TestCase_SqlString_ExecuteScalar_WithObject_Async()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ac;

            sql = "select count(1) from \"Te_BaseField\"";
            ac = Convert.ToInt32(await context.ExecuteScalarSqlStringAsync(sql));
            Assert.Equal(list.Count, ac);

            sql = "select count(1) from \"Te_BaseField\" where \"Id\"<={id}";
            ac = Convert.ToInt32(await context.ExecuteScalarSqlStringAsync(sql, new { id = 5 }));
            Assert.Equal(5, ac);

            sql = "select count(1) from \"Te_BaseField\"";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                ac = Convert.ToInt32(await context.ExecuteScalarSqlStringAsync(sql));
                context.CommitTrans();
            }
            Assert.Equal(list.Count, ac);
        }


        class TestDataParam
        {
            [DataParameter("p1")]
            public int InputData { get; set; }
            [DataParameter("p2", Direction = DataParameterMode.Output)]
            public int OutputData { get; set; }
        }

        [Fact]
        public void TestCase_StoreProcedure_Execute_OutParameter_WithObject()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            TestDataParam obj;
            sql = "sptest7";
            obj = new TestDataParam() { InputData = 5 };
            context.ExecuteNonQueryStoreProcedure(sql, obj);

            Assert.Equal(5, obj.OutputData);

            sql = "sptest7";
            obj = new TestDataParam() { InputData = 5 };
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.ExecuteNonQueryStoreProcedure(sql, obj);
                context.CommitTrans();
            }
            Assert.Equal(5, obj.OutputData);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_Execute_OutParameter_WithObject_Async()
        {
            var list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            TestDataParam obj;
            sql = "sptest7";
            obj = new TestDataParam() { InputData = 5 };
            await context.ExecuteNonQueryStoreProcedureAsync(sql, obj);

            Assert.Equal(5, obj.OutputData);

            sql = "sptest7";
            obj = new TestDataParam() { InputData = 5 };
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                await context.ExecuteNonQueryStoreProcedureAsync(sql, obj);
                context.CommitTrans();
            }
            Assert.Equal(5, obj.OutputData);
        }

        [Fact]
        public void TestCase_StoreProcedure_QueryList_WithObject()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;

            sql = "sptest1";
            listAc = context.QueryStoreProcedureList<TeBaseField>(sql);
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            listAc = context.QueryStoreProcedureList<TeBaseField>(sql, 5, 3);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest2";
            listAc = context.QueryStoreProcedureList<TeBaseField>(sql, new { p1 = 5, p2 = 8 });
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                listAc = context.QueryStoreProcedureList<TeBaseField>(sql);
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            var itemAc = context.QueryStoreProcedureFirst<TeBaseField>(sql);
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_QueryList_WithObject_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            List<TeBaseField> listEx;
            List<TeBaseField> listAc;
            string sql;

            sql = "sptest1";
            listAc = await context.QueryStoreProcedureListAsync<TeBaseField>(sql);
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            listAc = await context.QueryStoreProcedureListAsync<TeBaseField>(sql, 5, 3);
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest2";
            listAc = await context.QueryStoreProcedureListAsync<TeBaseField>(sql, new { p1 = 5, p2 = 8 });
            listEx = list.Where(x => x.Id > 5 && x.Id <= 8).ToList();
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                listAc = await context.QueryStoreProcedureListAsync<TeBaseField>(sql);
                context.CommitTrans();
            }
            listEx = list;
            AssertExtend.StrictEqual(listEx, listAc);

            sql = "sptest1";
            var itemAc = await context.QueryStoreProcedureFirstAsync<TeBaseField>(sql);
            var itemEx = list.First();
            AssertExtend.StrictEqual(itemEx, itemAc);
        }

        [Fact]
        public void TestCase_StoreProcedure_Execute_WithObject()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            TeBaseField user;

            sql = "sptest3";
            context.ExecuteNonQueryStoreProcedure(sql);
            user = context.SelectById<TeBaseField>(1);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);

            sql = "sptest4";
            context.ExecuteNonQueryStoreProcedure(sql, new { p1 = 2, p2 = "bcd" });
            user = context.SelectById<TeBaseField>(2);
            Assert.NotNull(user);
            Assert.Equal("bcd", user.VarcharField);

            sql = "sptest4";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                context.ExecuteNonQueryStoreProcedure(sql, new { p1 = 3, p2 = "abc" });
                context.CommitTrans();
            }
            user = context.SelectById<TeBaseField>(3);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_Execute_WithObject_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            TeBaseField user;

            sql = "sptest3";
            await context.ExecuteNonQueryStoreProcedureAsync(sql);
            user = context.SelectById<TeBaseField>(1);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);

            sql = "sptest4";
            await context.ExecuteNonQueryStoreProcedureAsync(sql, new { p1 = 2, p2 = "bcd" });
            user = context.SelectById<TeBaseField>(2);
            Assert.NotNull(user);
            Assert.Equal("bcd", user.VarcharField);

            sql = "sptest4";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                await context.ExecuteNonQueryStoreProcedureAsync(sql, new { p1 = 3, p2 = "abc" });
                context.CommitTrans();
            }
            user = context.SelectById<TeBaseField>(3);
            Assert.NotNull(user);
            Assert.Equal("abc", user.VarcharField);
        }

        [Fact]
        public void TestCase_StoreProcedure_ExecuteScalar_WithObject()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ac;

            sql = "sptest5";
            ac = Convert.ToInt32(context.ExecuteScalarStoreProcedure(sql));
            Assert.Equal(10, ac);

            sql = "sptest6";
            ac = Convert.ToInt32(context.ExecuteScalarStoreProcedure(sql, new { p1 = 5 }));
            Assert.Equal(5, ac);

            sql = "sptest5";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                ac = Convert.ToInt32(context.ExecuteScalarStoreProcedure(sql));
                context.CommitTrans();
            }
            Assert.Equal(10, ac);
        }

        [Fact]
        public async Task TestCase_StoreProcedure_ExecuteScalar_WithObject_Async()
        {
            List<TeBaseField> list = CreateAndInsertBaseFieldTableList(10);
            string sql;
            int ac;

            sql = "sptest5";
            ac = Convert.ToInt32(await context.ExecuteScalarStoreProcedureAsync(sql));
            Assert.Equal(10, ac);

            sql = "sptest6";
            ac = Convert.ToInt32(context.ExecuteScalarStoreProcedure(sql, new { p1 = 5 }));
            Assert.Equal(5, ac);

            sql = "sptest5";
            using (var trans = context.BeginTrans()) {
                //trans.BeginTrans();
                ac = Convert.ToInt32(await context.ExecuteScalarStoreProcedureAsync(sql));
                context.CommitTrans();
            }
            Assert.Equal(10, ac);
        }
    }


}
